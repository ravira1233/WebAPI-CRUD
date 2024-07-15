using Microsoft.EntityFrameworkCore;

namespace WebAPI_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
           
        }
        public DbSet<Employee> employees { get; set; }
}
}
    
