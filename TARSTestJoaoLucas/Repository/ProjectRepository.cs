using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;

namespace TARSTestJoaoLucas.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        { }

        public async Task<Worker> GetProjectWorker(int id)
        {
            return (await Get().Where(p => p.Id == id).Include(p => p.Worker).SingleOrDefaultAsync()).Worker;
        }
    }
}