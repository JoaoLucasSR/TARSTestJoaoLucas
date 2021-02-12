using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Repository;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly ILogger<WorkerController> _logger;
        private readonly IUnitOfWork _uof;

        public WorkerController(ILogger<WorkerController> logger, IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> Get([FromQuery] PaginationParameters paginationParameters)
        {
            var workers = await _uof.WorkerRepository.GetWorkersPagination(paginationParameters);

            var pageMetadata = new
            {
                workers.TotalCount,
                workers.PageSize,
                workers.CurrentPage,
                workers.TotalPages,
                workers.HasNext,
                workers.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMetadata));

            return workers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetId(int id)
        {
            var worker = await _uof.WorkerRepository.GetById(w => w.Id == id);

            if(worker == null)
                return NotFound();
            
            return worker;
        }

        [HttpGet("{id}/project")]
        public async Task<ActionResult<List<Project>>> GetProject(int id, [FromQuery] PaginationParameters paginationParameters)
        {
            var projects = await _uof.WorkerRepository.GetWorkerProjectsPagiation(id, paginationParameters);

            var pageMetadata = new
            {
                projects.TotalCount,
                projects.PageSize,
                projects.CurrentPage,
                projects.TotalPages,
                projects.HasNext,
                projects.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMetadata));

            if(projects == null)
                return NotFound();
            
            return projects;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Worker worker)
        {
            if(id != worker.Id)
                return BadRequest("Id's are not the same");

            _uof.WorkerRepository.Update(worker);

            try
            {
                await _uof.CommitAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!(await WorkerExists(id)))
                    return NotFound();
                else
                    _logger.LogError(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Worker>> Post([FromBody] Worker worker)
        {
            _uof.WorkerRepository.Add(worker);
            await _uof.CommitAsync();

            return CreatedAtAction("GetId", new { id = worker.Id}, worker);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Worker>> Delete(int id)
        {
            var worker = await _uof.WorkerRepository.GetById(w => w.Id == id);
            if(worker == null)
                return NotFound();
            
            _uof.WorkerRepository.Delete(worker);
            await _uof.CommitAsync();

            return worker;
        }

        private async Task<bool> WorkerExists(int id)
        {
            return (await _uof.WorkerRepository.GetById(w => w.Id == id)) != null;
        }
    }
}