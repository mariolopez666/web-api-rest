using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class RepositoryCategoria
    {
        private string cadenaConexion = null;
        private readonly IMapper mapper;
        public RepositoryCategoria(IConfiguration configuration, IMapper mapper)
        {
            cadenaConexion = configuration.GetConnectionString("DefaultConnection");
            this.mapper = mapper;
        }
    }
}
