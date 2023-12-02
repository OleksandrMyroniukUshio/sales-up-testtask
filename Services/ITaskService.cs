using sales_up.Models;

namespace sales_up.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync(string sortBy = "name", bool descending = false);
        Task AddTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskNameAsync(int id, string newName);
        Task UpdateTaskCompletionAsync(int id);
    }
}
