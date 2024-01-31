using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : BaseDomainModel
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate); /* Se utiliza expression por que la lista viene desde la base de datos es del tipo IQueryable*/
    
    /// <summary>
    /// Recupera de manera asíncrona una lista de entidades del tipo <typeparamref name="T"/>.
    /// </summary>
    /// <param name="predicate">Una expresión de predicado para filtrar las entidades. Por defecto es null, lo cual obtiene todas las entidades.</param>
    /// <param name="orderBy">Una función para ordenar las entidades. Por defecto es null, lo cual no altera el orden de las entidades.</param>
    /// <param name="includeString">Una cadena que representa las entidades relacionadas a incluir en la consulta para la carga anticipada. Por defecto es null.</param>
    /// <param name="disableTracking">Indica si se debe desactivar el seguimiento de cambios para las entidades recuperadas. Por defecto es true, lo que mejora el rendimiento cuando las entidades no se actualizan en el contexto actual.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene una lista de solo lectura de entidades.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                    string includeString = null,
                                    bool disableTracking = true);

    /// <summary>
    /// Realiza una consulta asincrónica en la base de datos y recupera una lista de entidades del tipo <typeparamref name="T"/> que cumplan con los criterios especificados.
    /// </summary>
    /// <param name="predicate">Expresión de predicado opcional para filtrar las entidades. Si es null, se recuperarán todas las entidades.</param>
    /// <param name="orderBy">Función opcional para ordenar las entidades resultantes. Si es null, el orden por defecto de la base de datos será aplicado.</param>
    /// <param name="includes">Lista opcional de expresiones para especificar las propiedades de navegación a incluir (carga ansiosa). Si es null, no se incluirán relaciones adicionales.</param>
    /// <param name="disableTracking">Indica si se deben rastrear los cambios en las entidades recuperadas. Un valor true deshabilita el seguimiento, lo cual puede mejorar el rendimiento para consultas de solo lectura.</param>
    /// <returns>Una tarea que, al completarse, proporcionará una lista de solo lectura con las entidades del tipo <typeparamref name="T"/> que coinciden con los criterios.</returns>
    public Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);
    
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
