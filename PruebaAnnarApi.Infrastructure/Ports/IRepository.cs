using PruebaAnnarApi.Domain.Entities;
using System.Linq.Expressions;

namespace CitasIPS.Infrastructure.Ports;

public interface IRepository<T> where T : DomainEntity
{
    Task<T> AddAsync(T entity);
    void Delete(T entity);

    Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        string? includeStringProperties,
        bool isTracking);

    Task<T> GetOneAsync(Guid id);
    void Update(T entity);
}
