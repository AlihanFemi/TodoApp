using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.Mappings;

namespace Todo.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> Todos { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new TodosMapping());
            modelBuilder.ApplyConfiguration(new TaskMapping());

            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=main_db",
                options => options.MigrationsAssembly(
                    typeof(DatabaseContext).Assembly.GetName().Name));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
