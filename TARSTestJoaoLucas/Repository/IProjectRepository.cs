using System.Threading.Tasks;
using TARSTestJoaoLucas.Models;

namespace TARSTestJoaoLucas.Repository
{
    public interface IProjectRepository
    {
        Task<Worker> GetProjectWorker(int id);
    }
}