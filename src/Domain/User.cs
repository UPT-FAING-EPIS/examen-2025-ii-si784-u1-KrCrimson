using System;

namespace Domain
{
    /// <summary>
    /// Representa un usuario del sistema de reservas de vuelos.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash de la contraseña del usuario.
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
