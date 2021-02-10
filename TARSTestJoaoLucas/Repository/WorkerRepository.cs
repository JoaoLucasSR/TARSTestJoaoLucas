using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;

namespace TARSTestJoaoLucas.Repository
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Project>> GetWorkerProjects(int id)
        {
            return (await Get().Where(w => w.Id == id).Include(w => w.Projects).SingleOrDefaultAsync()).Projects;
        }
    }
}