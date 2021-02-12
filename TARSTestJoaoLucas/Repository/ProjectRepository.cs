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
            var projects = Get().OrderBy(p => p.Id);
            if (projects == null)
                return null;
            return await PagedList<Project>.ToPagedList(projects, paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public async Task<Worker> GetProjectWorker(int id)
        {
            var project = await Get().Where(p => p.Id == id).Include(p => p.Worker).SingleOrDefaultAsync();
            if (project == null)
                return null;
            return project.Worker;
        }
    }
}