using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;

namespace TARSTestJoaoLucas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly AppDbContext _context;

        public ProjectController(ILogger<ProjectController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetId(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if(project == null)
                return NotFound();
            
            return project;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            if(id != project.Id)
                return BadRequest("Id's are not the same");

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!ProjectExists(id))
                    return NotFound();
                else
                    _logger.LogError(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetId", new { id = project.Id}, project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null)
                return NotFound();
            
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }
    }
}