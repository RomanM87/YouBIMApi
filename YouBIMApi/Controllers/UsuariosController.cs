using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using YouBIMApi.Models;
using YouBIMApi.Models.Dtos;
using YouBIMApi.Repository;

namespace YouBIMApi.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "YouBIMApi")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _userRepo;
        private readonly IMapper _mapper;
        

        public UsuariosController(IUsuarioRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        
        //Endpoint to obtain all active users
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _userRepo.GetUsuarios();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var lista in listaUsuarios)
            {
                if (lista.Activo == true)
                {
                    listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
                }                
            }
            return Ok(listaUsuariosDto);
        }


        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _userRepo.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        //Endpoint to create a new User(The user must be created Active)
        [HttpPost]
        public IActionResult CrearUsuario(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepo.FechaValida(usuarioDto.FechaNac))
            {
                ModelState.AddModelError("", "Ingrese una Fecha de Nacimiento valida");
                return StatusCode(404, ModelState);
            }

            if (_userRepo.ExisteUsuario(usuarioDto.NombreUsuario, usuarioDto.FechaNac))
            {
                ModelState.AddModelError("", "El Usuario ya existe");
                return StatusCode(404, ModelState);
            }

            var user = _mapper.Map<Usuario>(usuarioDto);
            user.Activo = true;

            if (!_userRepo.CrearUsuario(user))
            {
                ModelState.AddModelError("", $"No se pudo guardar el registro{user.NombreUsuario}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUsuario", new { usuarioId = user.IdUsuario }, user);
        }

        //Endpoint to update the User state (Active field)
        [HttpPatch]
        public IActionResult ActualizarUsuario( [FromBody] UsuarioUpdateDto usuarioDto)
        {
            Usuario userActual = _userRepo.GetUsuario(usuarioDto.IdUsuario);

            if (usuarioDto == null || usuarioDto.IdUsuario == 0 || userActual == null)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<Usuario>(userActual);            

            if (!_userRepo.ActualizarUsuario(user))
            {
                ModelState.AddModelError("", $"No se pudo actualizar el registro{user.IdUsuario}");
                return StatusCode(500, ModelState);
            }

            return Content("El Usuario fue actualizado. Id: "+ user.IdUsuario+", Active:"+user.Activo);
        }

        //Endpoint to delete users.
        [HttpDelete("{usuarioId:int}", Name = "BorrarUsuario")]
        public IActionResult BorrarUsuario(int usuarioId)
        {
            if (!_userRepo.ExisteUsuario(usuarioId))
            {
                ModelState.AddModelError("", "El Usuario que intenta eliminar no existe");
                return StatusCode(404, ModelState);
            }

            var usuario = _userRepo.GetUsuario(usuarioId);

            if (!_userRepo.BorrarUsuario(usuario))
            {
                ModelState.AddModelError("", $"No se pudo actualizar el registro{usuario.IdUsuario}");
                return StatusCode(500, ModelState);
            }

            return Content("Se elimino el Usuario Id: " + usuario.IdUsuario);
        }

    }
}
