using System.Threading.Tasks;

namespace TARSTestJoaoLucas.Repository
{
    public interface IUnitOfWork
    {
        IWorkerRepository WorkerRepository { get; }
        IProjectRepository ProjectRepository { get; }
        Task CommitAsync();
    }
}