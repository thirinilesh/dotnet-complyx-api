using AutoMapper;
using AutoMapper.Configuration;
using ComplyX.Shared.Data;
using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class UserProfile : AutoMapper.Profile
    {
         
        private readonly List<TypeMapConfiguration> _typeMapConfigs = [];
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                 
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
             
        }
      
        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>() =>
    CreateMapCore<TSource, TDestination>(MemberList.Destination);

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList memberList) =>
            CreateMapCore<TSource, TDestination>(memberList);
        private IMappingExpression<TSource, TDestination> CreateMapCore<TSource, TDestination>(MemberList memberList, bool projection = false)
        {
            MappingExpression<TSource, TDestination> mappingExp = new(memberList, projection);
            _typeMapConfigs.Add(mappingExp);
            return mappingExp;

        }

    }
}
