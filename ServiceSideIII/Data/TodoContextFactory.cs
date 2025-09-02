using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ServerSideIII.Data
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();

            // Sørg for at connection string matcher din appsettings.json
            optionsBuilder.UseSqlServer(
                "Server=(local)\\SQLEXPRESS;Database=TodoDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

            return new TodoContext(optionsBuilder.Options);
        }
    }
}