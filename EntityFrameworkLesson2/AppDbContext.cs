
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkLesson1
{
    public class AppDbContext : DbContext
    {
        private string _connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;";

        /*public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection_str);
        }
    }
}
