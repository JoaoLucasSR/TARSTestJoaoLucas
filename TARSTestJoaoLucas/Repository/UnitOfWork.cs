using System.Threading.Tasks;
using TARSTestJoaoLucas.Context;

namespace TARSTestJoaoLucas.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private WorkerRepository _workerRepository;
        private ProjectRepository _projectRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IWorkerRepository WorkerRepository
        {
            get
            {
                return _workerRepository = _workerRepository ?? new WorkerRepository(_context);
            }
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                return _projectRepository = _projectRepository ?? new ProjectRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}