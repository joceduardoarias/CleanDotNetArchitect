using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories;

// La clase UnitOfWork implementa la interfaz IUnitOfWork, proporcionando una implementación concreta.
public class UnitOfWork : IUnitOfWork
{
    // Un hash table para almacenar las instancias de repositorios creadas.
    private Hashtable _respositories;
    // Contexto de la base de datos, usado para realizar las operaciones de persistencia.
    private readonly StreamerDbContext _context;

    // Constructor que inyecta el contexto de la base de datos.
    public UnitOfWork(StreamerDbContext context)
    {
        _context = context;
    }

    // Método para completar y guardar los cambios realizados en la base de datos.
    public async Task<int> Complete()
    {
        // Guarda los cambios en el contexto de la base de datos de manera asíncrona y retorna el número de entidades afectadas.
        return await _context.SaveChangesAsync();
    }

    // Método para liberar los recursos del contexto de la base de datos.
    public void Dispose()
    {
        _context.Dispose();
    }

    // Método genérico para obtener o crear una instancia de repositorio para una entidad específica.
    public IAsyncRepository<TEntity> Respository<TEntity>() where TEntity : BaseDomainModel
    {
        // Si _repositories es null, se inicializa.
        if (_respositories == null)
        {
            _respositories = new Hashtable();
        }
        // Obtiene el nombre del tipo de la entidad.
        var type = typeof(TEntity).Name;

        // Si no existe un repositorio para esa entidad, lo crea y lo agrega al hash table.
        if (!_respositories.ContainsKey(type))
        {
            // Define el tipo de repositorio a crear basado en la entidad específica RepositoryBase.
            var repositoryType = typeof(RepositoryBase<>);
            // Crea una instancia del repositorio utilizando el contexto de la base de datos.
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            // Agrega la instancia del repositorio al hash table.
            _respositories.Add(type, repositoryInstance);
        }

        // Retorna la instancia del repositorio específico para la entidad solicitada.
        return (IAsyncRepository<TEntity>)_respositories[type];
    }
}
