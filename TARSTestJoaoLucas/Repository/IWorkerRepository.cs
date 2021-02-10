using System.Threading.Tasks;
using System.Collections.Generic;
using TARSTestJoaoLucas.Models;

namespace TARSTestJoaoLucas.Repository
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<IEnumerable<Project>> GetWorkerProjects(int id);
    }
}