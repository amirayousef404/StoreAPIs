using AutoMapper;
using Store.G04.Core.Dtos.Baskets;
using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        { 
            CreateMap<CustomerBsaket, CustomerBsaketDto>().ReverseMap();
        }
    }
}
