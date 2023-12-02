using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using sales_up.Data;
using sales_up.Models;

namespace sales_up.Services
{
    public class TaskService : ITaskService
    {
        private readonly TasksDbContext _dbContext;
        public TaskService(TasksDbContext dbContext) { 
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync(string sortBy = "name", bool descending = false)
        {
            var query = _dbContext.Tasks.AsQueryable();
            switch (sortBy.ToLower())
            {
                case "name":
                    query = descending ? query.OrderByDescending(t => t.Name) : query.OrderBy(t => t.Name);
                    break;
                case "status":
                    query = descending ? query.OrderByDescending(t => t.IsCompleted) : query.OrderBy(t => t.IsCompleted);
                    break;
            }

            return await query.ToListAsync();
        }
        public async Task AddTaskAsync(TaskItem task)
        {
            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskNameAsync(int id, string newName)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task is not null)
            {
                task.Name = newName;
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task UpdateTaskCompletionAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task is not null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _dbContext.SaveChangesAsync();
            }
            else Console.WriteLine("nuh uh");
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
