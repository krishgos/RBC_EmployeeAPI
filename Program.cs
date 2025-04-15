using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBC_EmployeeAPI_POC.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:EmployeeConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Following Line is to Test the Health and Functionality of the API
app.UseMiddleware<RBC_EmployeeAPI_POC.TestMiddleware>();

/**************************************************************
 * Following Lines Of Code are to create the Minimal API
 * ************************************************************/

const string BASEURL = "api/employees";

app.MapGet($"{BASEURL}/{{employeeNumber}}", async (int employeeNumber, HttpContext context, DataContext data) =>
{
    var employee = await data.Employees.FindAsync(employeeNumber);    
    return employee != null ? Results.Ok(employee) : Results.NotFound();
});

app.MapGet(BASEURL, async (HttpContext context, DataContext data) =>
{
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Employee>>(data.Employees));
});

app.MapGet($"{BASEURL}/search", async ([FromQuery] string name, DataContext data) =>
{
    var results = await data.Employees
        .Where(e => e.EmployeeName.Contains(name))
        .ToListAsync();
    return Results.Ok(results);
});

app.MapGet($"{BASEURL}/page", async (DataContext data, [FromQuery] int page = 1, [FromQuery] int pageSize = 5) =>
{
    var skip = (page - 1) * pageSize;
    var employees = await data.Employees
        .Skip(skip)
        .Take(pageSize)
        .ToListAsync();
    return Results.Ok(employees);
});

app.MapPost(BASEURL, async ([FromBody] Employee employee, DataContext data) =>
{    
    data.Employees.Add(employee);
    await data.SaveChangesAsync();
    return Results.Created($"{BASEURL}/{employee.EmployeeNumber}", employee);
});

app.MapPut($"{BASEURL}/{{employeeNumber}}", async (int employeeNumber, [FromBody] Employee updated, DataContext data) =>
{
    var employee = await data.Employees.FindAsync(employeeNumber);
    if (employee == null) return Results.NotFound();

    employee.EmployeeName = updated.EmployeeName;
    employee.HourlyRate = updated.HourlyRate;
    employee.HoursWorked = updated.HoursWorked; 

    await data.SaveChangesAsync();
    return Results.Ok(employee);
});

app.MapDelete($"{BASEURL}/{{employeeNumber}}", async (int employeeNumber, DataContext data) =>
{
    var employee = await data.Employees.FindAsync(employeeNumber);
    if (employee == null) return Results.NotFound();

    data.Employees.Remove(employee);
    await data.SaveChangesAsync();
    return Results.NoContent();
});

/**************************************************************
 * The above is the end of Minimal API Code
 * ************************************************************/

//app.MapGet("/", () => "Hello World!");
var context = app.Services.CreateScope().ServiceProvider
.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
