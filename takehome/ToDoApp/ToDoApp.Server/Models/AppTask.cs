using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Server.Models
{
    public class AppTask
    {
        [Key]
        public string UUID { get; set; } = string.Empty;
        public long EpochTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}