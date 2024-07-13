using PruebaAnnarApi.Domain.Interfaces;

namespace PruebaAnnarApi.Domain.Ports
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
