using PruebaAnnarApi.Domain.Interfaces;
using PruebaAnnarApi.Domain.Ports;
using PruebaAnnarApi.Infrastructure.Persistence;

namespace PruebaAnnarApi.Infrastructure.Adapters
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _context = dataContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
