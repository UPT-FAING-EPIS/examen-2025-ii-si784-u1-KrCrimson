using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure
{
    /// <summary>
    /// Interfaz para el repositorio de usuarios.
    /// </summary>
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
