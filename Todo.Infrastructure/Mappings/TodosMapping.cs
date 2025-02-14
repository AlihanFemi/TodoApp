using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Models;

namespace Todo.Infrastructure.Mappings
{
    internal class TodosMapping : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.ToTable("users", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Description).HasColumnName("description").IsRequired();
            builder.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();
            builder.Property(e => e.Date).HasColumnName("date").IsRequired();
            builder.Property(e => e.NumberOfTasks).HasColumnName("num_of_tasks").IsRequired();

            builder.HasOne(e => e.Owner)
                .WithMany(e => e.Todos)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Tasks)
                .WithOne(e => e.Holder)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
