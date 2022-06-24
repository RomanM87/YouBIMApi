using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouBIMApi.Data;
using YouBIMApi.Models;

namespace YouBIMApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _bd;
        public UsuarioRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public Usuario GetUsuario(int UsuarioId)
        {
            return _bd.Usuario.FirstOrDefault(c => c.IdUsuario == UsuarioId);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(c => c.NombreUsuario).ToList();
        }
        public bool ExisteUsuario(string usuario, DateTime fechaNac)
        {
            if (_bd.Usuario.Any(c => c.NombreUsuario == usuario && c.FechaNac == fechaNac))
            {
                return true;
            }

            return false;
        }

        public bool FechaValida(DateTime fechaNac)
        {
            DateTime testFecha = new DateTime(0001, 01, 01);
            if (fechaNac > testFecha)
            {
                return true;
            }
            return false;
        }

        public bool CrearUsuario(Usuario usuario)
        {
            _bd.Usuario.Add(usuario);
            return Guardar();
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            usuario.Activo = usuario.Activo ? false : true ;

            _bd.Usuario.Update(usuario); 
            return Guardar();
        }

        public bool BorrarUsuario(Usuario usuario)
        {
            _bd.Usuario.Remove(usuario);
            return Guardar();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public bool ExisteUsuario(int usuarioId)
        {
            return _bd.Usuario.Any(c => c.IdUsuario == usuarioId);
        }
    }
}
