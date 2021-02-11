using System.Threading.Tasks;
using System.Collections.Generic;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Repository
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<IEnumerable<Project>> GetWorkerProjectsPagiation(int id, PaginationParameters paginationParameters);
        Task<IEnumerable<Project>> GetWorkerProjects(int id);
    }
}