using System;

namespace Catalog.Order.Domain.Common.Abstractions;

public interface IGenericRepository<TDomain, TResponse> 
    where TDomain : class 
    where TResponse : class
{
    // Escritura: Siempre usamos el Dominio
    Task CreateAsync(TDomain domain, CancellationToken ct = default);
    Task UpdateAsync(TDomain domain, CancellationToken ct = default);
    
    // Borrado: Retornamos Task para poder hacer await
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    
    // Lectura: Retornamos el DTO (Response) para mayor ligereza
    Task<Result<TResponse>> FindByIdAsync(Guid id, CancellationToken ct = default); 
    Task<Result<List<TResponse>>> FindAllAsync(int page, int size, CancellationToken ct = default);
}