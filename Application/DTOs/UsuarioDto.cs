using System;

namespace MantaroInclusivo.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string PerfilAccesibilidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }

    public class UsuarioRegistroDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string PerfilAccesibilidad { get; set; }
    }

    public class UsuarioLoginDto
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}