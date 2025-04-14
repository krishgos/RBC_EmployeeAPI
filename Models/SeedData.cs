using Microsoft.EntityFrameworkCore;

namespace RBC_EmployeeAPI_POC.Models
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Employees.Count() == 0)
            {
                //Supplier s1 = new Supplier { Name = "Splash Dudes", City = "San Jose" };
                //Supplier s2 = new Supplier { Name = "Soccer Town", City = "Chicago" };
                //Supplier s3 = new Supplier { Name = "Chess Co", City = "New York" };

                //Category c1 = new Category { Name = "Watersports" };
                //Category c2 = new Category { Name = "Soccer" };
                //Category c3 = new Category { Name = "Chess" };

                context.Employees.AddRange(
                    new Employee
                    {                        
                        EmployeeName = "John Doe",
                        HourlyRate = 15.0M,
                        HoursWorked = 10.5,
                        TotalPay = 15.0M * 10.5M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Little Jack Horner",
                        HourlyRate = 17.0M,
                        HoursWorked = 10.0,
                        TotalPay = 17.0M * 10.0M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Socretes",
                        HourlyRate = 20.0M,
                        HoursWorked = 10.0,
                        TotalPay = 20.0M * 10.0M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Plato",
                        HourlyRate = 10.0M,
                        HoursWorked = 35.0,
                        TotalPay = 10.0M * 35.0M
                    },
                   new Employee
                   {                       
                       EmployeeName = "Hagel",
                       HourlyRate = 20.0M,
                       HoursWorked = 10.0,
                       TotalPay = 20.0M * 10.0M
                   },
                    new Employee
                    {                        
                        EmployeeName = "Voltair",
                        HourlyRate = 25.0M,
                        HoursWorked = 20.0,
                        TotalPay = 25.0M * 20.0M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Karl Marx",
                        HourlyRate = 27.5M,
                        HoursWorked = 20.0,
                        TotalPay = 27.5M * 20.0M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Fredrick Engles",
                        HourlyRate = 19.5M,
                        HoursWorked = 20.0,
                        TotalPay = 19.5M * 20.0M
                    },
                    new Employee
                    {                        
                        EmployeeName = "Bertrand Russell",
                        HourlyRate = 11.5M,
                        HoursWorked = 35.5,
                        TotalPay = 11.5M * 35.5M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
