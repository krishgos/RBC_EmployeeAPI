using Microsoft.EntityFrameworkCore;

namespace RBC_EmployeeAPI_POC.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
        : base(opts) { }
        public DbSet<Employee> Employees => Set<Employee>();        
    }
}
