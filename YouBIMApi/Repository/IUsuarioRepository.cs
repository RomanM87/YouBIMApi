using System;
using System.Collections.Generic;
using YouBIMApi.Models;

namespace YouBIMApi.Repository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int UsuarioId);
        bool ExisteUsuario(string usuario, DateTime fechaNac);
        bool ExisteUsuario(int usuarioId);
        bool FechaValida( DateTime fechaNac);
        bool CrearUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);
        bool BorrarUsuario(Usuario usuario);
        bool Guardar();
    }
}
