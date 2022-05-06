using AutoMapper;
using DTO;
using EntityLayer;

namespace DataAccessLayer.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
        }
    }
}
