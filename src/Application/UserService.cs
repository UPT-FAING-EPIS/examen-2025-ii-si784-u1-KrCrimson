using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    /// <summary>
    /// Servicio para la lógica de negocio relacionada con usuarios.
    /// </summary>
    public class UserService
    {
        private readonly List<User> _users;

        /// <summary>
        /// Inicializa el servicio con una lista de usuarios (simulación).
        /// </summary>
        public UserService(List<User> users)
        {
            _users = users;
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        public (User? user, string? error) CreateUser(string name, string email, string passwordHash)
        {
            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return (null, "El correo electrónico ya está registrado.");
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = passwordHash
            };
            var validationError = user.Validate();
            if (validationError != null)
                return (null, validationError);
            _users.Add(user);
            return (user, null);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public User? GetUserById(Guid userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
