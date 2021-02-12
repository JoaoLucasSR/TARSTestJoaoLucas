using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Repository
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(AppDbContext context) : base(context)
        { }

        public async Task<PagedList<Project>> GetWorkerProjectsPagiation(int id, PaginationParameters paginationParameters)
        {
            var worker = await Get().Where(w => w.Id == id).Include(w => w.Projects.OrderBy(p => p.Id)).SingleOrDefaultAsync();
            if (worker == null)
                return null;
            return await PagedList<Project>.ToPagedList(worker.Projects, paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<PagedList<Worker>> GetWorkersPagination(PaginationParameters paginationParameters)
        {
            var workers = Get().OrderBy(w => w.Id);
            if (workers == null)
                return null;
            return await PagedList<Worker>.ToPagedList(workers, paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<IEnumerable<Project>> GetWorkerProjects(int id)
        {
            var worker = await Get().Where(w => w.Id == id).Include(w => w.Projects).SingleOrDefaultAsync();
            return worker.Projects;
        }
    }
}