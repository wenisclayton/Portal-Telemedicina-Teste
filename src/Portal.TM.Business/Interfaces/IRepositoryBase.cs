using System.Linq.Expressions;
using Portal.TM.Business.Models;

namespace Portal.TM.Business.Interfaces;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : BaseEntity
{
    #region Métodos Async

    Task AddAsync(TEntity entity);
    Task AddRangeAsync(List<TEntity> entities);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(Guid id);
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChangesAsync();

    #endregion

    #region Métodos Sync
    void Add(TEntity entity);
    void AddRange(List<TEntity> entities);
    TEntity? GetById(Guid id);
    List<TEntity> GetAll();
    void Update(TEntity entity);
    void Remove(Guid id);
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
    int SaveChanges();
    IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Queryable();
    #endregion
}