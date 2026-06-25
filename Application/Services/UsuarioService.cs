using System;
using System.Threading.Tasks;
using MantaroInclusivo.Domain.Entities;
using MantaroInclusivo.Domain.Interfaces;
using MantaroInclusivo.Application.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace MantaroInclusivo.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDto> RegistrarUsuario(UsuarioRegistroDto registroDto)
        {
            if (await _usuarioRepository.ExisteEmail(registroDto.Email))
                throw new InvalidOperationException("El correo electrónico ya está registrado.");

            var usuario = new Usuario
            {
                Nombres = registroDto.Nombres,
                Apellidos = registroDto.Apellidos,
                Email = registroDto.Email,
                Telefono = registroDto.Telefono,
                ContraseñaHash = HashContraseña(registroDto.Contraseña),
                PerfilAccesibilidad = registroDto.PerfilAccesibilidad,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var id = await _usuarioRepository.CrearUsuario(usuario);
            usuario.Id = id;

            return MapToDto(usuario);
        }

        public async Task<UsuarioDto> LoginUsuario(UsuarioLoginDto loginDto)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorEmail(loginDto.Email);
            
            if (usuario == null || !usuario.Activo)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var hashContraseña = HashContraseña(loginDto.Contraseña);
            if (usuario.ContraseñaHash != hashContraseña)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            return MapToDto(usuario);
        }

        public async Task<UsuarioDto> ObtenerUsuario(int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorId(id);
            return usuario != null ? MapToDto(usuario) : null;
        }

        public async Task<string> ObtenerPerfilUsuario(int id)
        {
            return await _usuarioRepository.ObtenerPerfilUsuario(id);
        }

        private static string HashContraseña(string contraseña)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
            return Convert.ToBase64String(hashedBytes);
        }

        private static UsuarioDto MapToDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                PerfilAccesibilidad = usuario.PerfilAccesibilidad,
                FechaRegistro = usuario.FechaRegistro,
                Activo = usuario.Activo
            };
        }
    }
}