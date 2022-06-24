using AutoMapper;
using YouBIMApi.Models;
using YouBIMApi.Models.Dtos;

namespace YouBIMApi.Mapper
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();
        }
    }
}
