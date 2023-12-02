using System.ComponentModel.DataAnnotations;

namespace sales_up.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Undefined task";
        [Required]
        public bool IsCompleted { get; set; }
    }
}
