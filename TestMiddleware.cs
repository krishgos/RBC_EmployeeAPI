using RBC_EmployeeAPI_POC.Models;

namespace RBC_EmployeeAPI_POC
{
    public class TestMiddleware
    {
        private RequestDelegate nextDelegate;

        public TestMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if (context.Request.Path == "/test")
            {
                await context.Response.WriteAsync(
                    $"There are {dataContext.Employees.Count()} employees\n");                
            }
            else
            {
                await nextDelegate(context);
            }
        }
    }
}
