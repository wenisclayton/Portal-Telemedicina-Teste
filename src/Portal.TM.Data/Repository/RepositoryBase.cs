using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Portal.TM.Business.Interfaces;
using Portal.TM.Business.Models;
using Portal.TM.Data.Context;

namespace Portal.TM.Data.Repository;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly MyDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected RepositoryBase(MyDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    #region Métodos Async
    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await SaveChangesAsync();
    }
    public async Task AddRangeAsync(List<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await SaveChangesAsync();
    }
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChangesAsync();
    }
    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Db.SaveChangesAsync();
    }

    #endregion

    #region Métodos Sync
    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
        SaveChanges();
    }

    public void AddRange(List<TEntity> entities)
    {
        DbSet.AddRange(entities);
        SaveChanges();
    }

    public TEntity? GetById(Guid id)
    {
        return DbSet.Find(id);
    }

    public List<TEntity> GetAll()
    {
        return DbSet.ToList();
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
        SaveChanges();
    }

    public void Remove(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        SaveChanges();
    }

    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AsNoTracking().Where(predicate).ToList();
    }

    public int SaveChanges()
    {
        return Db.SaveChanges();
    }

    public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AsNoTracking().Where(predicate);
    }

    public IQueryable<TEntity> Queryable()
    {
        return DbSet.AsQueryable<TEntity>().AsQueryable();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
    #endregion
}

