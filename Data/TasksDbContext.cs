using Microsoft.EntityFrameworkCore;
using sales_up.Models;

namespace sales_up.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set;}
    }
}
