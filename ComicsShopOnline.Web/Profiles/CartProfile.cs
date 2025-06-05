using AutoMapper;
using ComicsShopOnline.Domain.Entities;
using ComicsShopOnline.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicsShopOnline.Web.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItemDBTable, CartItemViewModel>()
                .ForMember(dest => dest.ComicName, opt => opt.MapFrom(src => src.Comic.Name))
                .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.Comic.CoverURl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Comic.Price));
        }
    }
}