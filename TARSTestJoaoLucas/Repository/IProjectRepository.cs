using System.Threading.Tasks;
using TARSTestJoaoLucas.Models;
using TARSTestJoaoLucas.Pagination;

namespace TARSTestJoaoLucas.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<PagedList<Project>> GetProjectsPagination(PaginationParameters paginationParameters);
        Task<Worker> GetProjectWorker(int id);
    }
}