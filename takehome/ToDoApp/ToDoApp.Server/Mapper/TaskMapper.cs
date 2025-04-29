using ToDoApp.Server.Dto;
using ToDoApp.Server.Models;

namespace ToDoApp.Server.Mapper
{
    public static class TaskMapper
    {
        public static TaskDto ToTaskDto(AppTask taskModel)
        {
            return new TaskDto
            {
                UUID = taskModel.UUID,
                EpochTime = taskModel.EpochTime,
                Name = taskModel.Name,
                Description = taskModel.Description,
                IsActive = taskModel.IsActive
            };
        }

        public static AppTask ToTaskFromCreateDto(this CreateTaskRequest taskDto)
        {
            return new AppTask
            {
                UUID = taskDto.UUID,
                EpochTime = taskDto.EpochTime,
                Name = taskDto.Name,
                Description = taskDto.Description,
                IsActive = taskDto.IsActive
            };
        }
    }
}