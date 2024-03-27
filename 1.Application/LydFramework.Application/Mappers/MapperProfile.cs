using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Mappers
{
    public class MapperProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //这里写映射规则
            //config.ForType<EDUC_USER, EducUserDto>()
            //    .Map(dest => dest.Mobile, src => src.u_mobile)
            //    .Map(dest => dest.Password, src => src.u_password)
            //    .Map(dest => dest.Qq, src => src.u_qq);
        }
    }
}
