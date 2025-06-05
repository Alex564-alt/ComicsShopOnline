using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ComicsShopOnline.Domain.Entities;
using ComicsShopOnline.Domain.Entities.User;
using ComicsShopOnline.Web.Models;

namespace ComicsShopOnline.Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterViewModel, UserDBTable>()
                //ignore UserDNTable password
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}