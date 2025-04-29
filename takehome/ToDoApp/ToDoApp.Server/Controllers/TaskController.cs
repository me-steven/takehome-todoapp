using Microsoft.AspNetCore.Mvc;
using ToDoApp.Server.Data;
using ToDoApp.Server.Mapper;
using ToDoApp.Server.Dto;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Server.Models;

namespace ToDoApp.Server.Controllers
{
    [Route("/api/tasks")]
    [ApiController]
    public class TaskController: ControllerBase 
    {
        private readonly ApplicationDBContext _context;
        public TaskController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet()]

        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();

            var taskDto = tasks.Select(t => TaskMapper.ToTaskDto(t));

            return Ok(taskDto);
        }
        
        [HttpGet("{uuid}")]
        public async Task<IActionResult> GetTaskById(string uuid)
        {
            var task = await _context.Tasks.FindAsync(uuid);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(TaskMapper.ToTaskDto(task));
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest taskDto)
        {
            var newTask = taskDto.ToTaskFromCreateDto();
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskById), new { uuid = newTask.UUID }, TaskMapper.ToTaskDto(newTask));
        }

        [HttpPut()]
        [Route("{uuid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] string uuid, [FromBody] UpdateTaskDto taskDto)
        {
            var taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(t => t.UUID == uuid);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            taskToUpdate.IsActive = taskDto.IsActive;

            await _context.SaveChangesAsync();
            return Ok(TaskMapper.ToTaskDto(taskToUpdate));
        }

        [HttpDelete]
        [Route("{uuid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] string uuid)
        {
            var taskToDelete = await _context.Tasks.FirstOrDefaultAsync(t => t.UUID == uuid);
            if (taskToDelete == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}