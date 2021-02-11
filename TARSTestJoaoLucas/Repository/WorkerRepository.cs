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
            return await PagedList<Project>.ToPagedList((await Get().Where(w => w.Id == id).Include(w => w.Projects.OrderBy(p => p.Id)).SingleOrDefaultAsync()).Projects.AsQueryable(), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<PagedList<Worker>> GetWorkersPagination(PaginationParameters paginationParameters)
        {
            return await PagedList<Worker>.ToPagedList(Get().OrderBy(w => w.Id), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<IEnumerable<Project>> GetWorkerProjects(int id)
        {
            return (await Get().Where(w => w.Id == id).Include(w => w.Projects).SingleOrDefaultAsync()).Projects;
        }
    }
}