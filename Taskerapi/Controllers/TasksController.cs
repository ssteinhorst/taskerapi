using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TaskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        //[HttpPost]
        //public async Task<ActionResult<Task>> CreateTask(Task task)
        //{
        //    // TODO: add handling multiple tasks at once
        //    // TODO: add data validation

        //    _context.Tasks.Add(task);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        //}
        [HttpPost]
        public IActionResult CreateTasks(List<Task> tasks)
        {
            if (tasks == null || tasks.Count == 0)
            {
                return BadRequest("No tasks provided.");
            }

            // Validate the data schema for each task
            foreach (var task in tasks)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(task);
                if (!Validator.TryValidateObject(task, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults.Select(vr => vr.ErrorMessage));
                }
            }

            // Map and persist the tasks
            var newTasks = tasks.Select(taskDto => new Task
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                ExpirationDate = taskDto.ExpirationDate,
                IsComplete = taskDto.IsComplete
            });

            _context.Tasks.AddRange(newTasks);
            _context.SaveChanges();

            return Ok("Tasks created successfully.");
        }


        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Task task)
        {
            // TODO: add data validation

            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
