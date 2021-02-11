using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Context;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        { }

        public async Task<PagedList<Project>> GetProjectsPagination(PaginationParameters paginationParameters)
        {
            return await PagedList<Project>.ToPagedList(Get().OrderBy(p => p.Id), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<Worker> GetProjectWorker(int id)
        {
            return (await Get().Where(p => p.Id == id).Include(p => p.Worker).SingleOrDefaultAsync()).Worker;
        }
    }
}