using System.Threading.Tasks;
using System.Collections.Generic;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsPagination(PaginationParameters paginationParameters);
        Task<Worker> GetProjectWorker(int id);
    }
}