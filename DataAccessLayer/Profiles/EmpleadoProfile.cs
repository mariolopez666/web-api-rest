using AutoMapper;
using DTO;
using EntityLayer;

namespace DataAccessLayer.Profiles
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile()
        {
            CreateMap<Empleado, EmpleadoDto>().ReverseMap();
        }
    }
}
