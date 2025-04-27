using ToDoApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Server.Data 
{
    public class ApplicationDBContext : DbContext {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<appTask> Tasks { get; set; } = null!;
    }
}