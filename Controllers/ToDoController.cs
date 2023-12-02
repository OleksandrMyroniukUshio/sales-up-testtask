using Microsoft.AspNetCore.Mvc;
using sales_up.Models;
using sales_up.Services;
using System;
using System.Threading.Tasks;

namespace sales_up.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ITaskService _taskService;

        public ToDoController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTasks(string sortBy = "name", bool descending = false)
        {
            var tasks = await _taskService.GetTaskItemsAsync(sortBy, descending);
            return Json(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                await _taskService.AddTaskAsync(taskItem);
                return Json(new { success = true, message = "Task added successfully" });
            }

            return Json(new { success = false, message = "Invalid task data" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return Json(new { success = true, message = "Task deleted successfully" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't remove the task" });
            }
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateTaskName(int id, [FromBody] string newName)
        {
            try
            {
                await _taskService.UpdateTaskNameAsync(id, newName);
                return Json(new { success = true, message = "Task name updated successfully" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't update the task name" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ToggleCompletion(int id)
        {
            Console.WriteLine(id);
            try
            {
                await _taskService.UpdateTaskCompletionAsync(id);
                return Json(new { success = true, message = "Task updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
