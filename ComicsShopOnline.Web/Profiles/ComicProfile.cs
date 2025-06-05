using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ComicsShopOnline.Domain.Entities;
using ComicsShopOnline.Domain.Entities.Comic;
using ComicsShopOnline.Web.Models;

namespace ComicsShopOnline.Web.Profiles
{
    public class ComicProfile : Profile
    {
        public ComicProfile()
        {
            CreateMap<ComicDBTable, ComicViewModel>()
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.ToString()));
        }
    }
}