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
    public class WorkerController : ControllerBase
    {
        private readonly ILogger<WorkerController> _logger;
        private readonly AppDbContext _context;

        public WorkerController(ILogger<WorkerController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> Get()
        {
            return await _context.Workers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetId(int id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if(worker == null)
                return NotFound();
            
            return worker;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Worker worker)
        {
            if(id != worker.Id)
                return BadRequest("Id's are not the same");

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if(!WorkerExists(id))
                    return NotFound();
                else
                    _logger.LogError(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Worker>> Post([FromBody] Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetId", new { id = worker.Id}, worker);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Worker>> Delete(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if(worker == null)
                return NotFound();
            
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(w => w.Id == id);
        }
    }
}