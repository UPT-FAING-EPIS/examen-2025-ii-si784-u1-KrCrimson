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

        /// <summary>
        /// Valida los datos del usuario. Retorna un mensaje de error si hay datos inválidos, o null si todo es válido.
        /// </summary>
        public string? Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return "El nombre es obligatorio.";
            if (string.IsNullOrWhiteSpace(Email))
                return "El correo electrónico es obligatorio.";
            if (!Email.Contains("@"))
                return "El correo electrónico no tiene un formato válido.";
            if (string.IsNullOrWhiteSpace(PasswordHash))
                return "La contraseña es obligatoria.";
            return null;
        }
    }
}
