using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Repository;

namespace TARSTestJoaoLucas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IUnitOfWork _uof;

        public ProjectController(ILogger<ProjectController> logger, IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            return await _uof.ProjectRepository.Get().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetId(int id)
        {
            var project = await _uof.ProjectRepository.GetById(p => p.Id == id);

            if(project == null)
                return NotFound();
            
            return project;
        }

        [HttpGet("{id}/worker")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            var worker = await _uof.ProjectRepository.GetProjectWorker(id);

            if(worker == null)
                return NotFound();
            
            return worker;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            if(id != project.Id)
                return BadRequest("Id's are not the same");

            _uof.ProjectRepository.Update(project);

            try
            {
                await _uof.CommitAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!(await ProjectExists(id)))
                    return NotFound();
                else
                    _logger.LogError(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] Project project)
        {
            _uof.ProjectRepository.Add(project);
            await _uof.CommitAsync();

            return CreatedAtAction("GetId", new { id = project.Id}, project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> Delete(int id)
        {
            var project = await _uof.ProjectRepository.GetById(p => p.Id == id);
            if(project == null)
                return NotFound();
            
            _uof.ProjectRepository.Delete(project);
            await _uof.CommitAsync();

            return project;
        }

        private async Task<bool> ProjectExists(int id)
        {
            return (await _uof.ProjectRepository.GetById(p => p.Id == id)) != null;
        }
    }
}