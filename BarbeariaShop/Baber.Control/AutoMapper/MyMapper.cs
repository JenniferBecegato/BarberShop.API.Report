using AutoMapper;
using Baber.Model.Entity;
using Baber.Model.Request.Registros;

namespace Baber.Control.AutoMapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Registros,Faturamento>();

        }
    }
}
