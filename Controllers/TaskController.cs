﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeManagement.Service;
using TimeManagement.Data;
using Task = TimeManagement.Data.Task;

namespace TimeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

       
        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _taskService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("ClientByProjectId/{projectId}")]
        public async Task<IActionResult> GetClientByProjectId(int projectId)
        {
            var client = await _taskService.GetClientByProjectIdAsync(projectId);
            if (client == null)
            {
                return NotFound("Client not found for the selected project.");
            }

            return Ok(client);
        }

        [HttpGet("ProjectTask/{ProjectId}")]
        public async Task<IActionResult>GetTaskNames(int ProjectId)
        {
            var taskName = await _taskService.GetTaskNameByProjectIdAsync(ProjectId);
            return Ok(taskName);
        }

      
        [HttpPost("SaveTaskDetails")]
        public async Task<IActionResult> SaveTaskDetails([FromBody] Task taskDetails)
        {
            if (taskDetails == null)
            {
                return BadRequest("Invalid task details.");
            }

            var taskId = await _taskService.SaveTaskDetailsAsync(taskDetails);
            return CreatedAtAction(nameof(SaveTaskDetails), new { id = taskId });
        }
    }
}
