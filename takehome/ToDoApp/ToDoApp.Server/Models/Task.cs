namespace ToDoApp.Server.Models
{
    public class appTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public long dateCreated { get; set; }

        public string UUID { get; set; } = string.Empty;

    }
}