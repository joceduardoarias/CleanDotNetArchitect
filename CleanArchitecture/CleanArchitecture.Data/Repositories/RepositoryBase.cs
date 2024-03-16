using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
{
    protected readonly StreamerDbContext _context;

    public RepositoryBase(StreamerDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public void AddEntity(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);

        await _context.SaveChangesAsync();
    }

    public void DeleteEntity(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    // Método para actualizar una entidad en la base de datos.
    public void UpdateEntity(T entity)
    {
        // Asegura que la instancia de la entidad sea reconocida por el contexto de Entity Framework,
        // pero sin cambiar su estado en el contexto si ya está siendo rastreada.
        _context.Set<T>().Attach(entity);

        // Marca el estado de la entidad específica como 'Modified'.
        // Entity Framework ahora sabe que la entidad ha sido modificada y necesita ser actualizada
        // en la base de datos cuando se llame a SaveChanges o SaveChangesAsync.
        _context.Entry(entity).State = EntityState.Modified;
    }
}

