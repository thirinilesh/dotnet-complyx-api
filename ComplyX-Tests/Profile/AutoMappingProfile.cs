using Castle.Core.Resource;
using ComplyX_Businesss.Models;
using Microsoft.SharePoint.Client;

namespace ComplyX_Tests.Profile
{
    
    public  class AutoMappingProfile : AutoMapper.Profile
    {
        const int GROUP_TYPE_SINGLE = 1;
        const int GROUP_TYPE_CONSOLIDATED = 2;
        const int GROUP_TYPE_CONTROLLED = 3;


        public AutoMappingProfile()
        {
            #region Company Mappings
            _ = CreateMap<RegisterUser, UpdateCompanyRequest>()
                 .ForMember(x => x.UserName, y => y.MapFrom(z => z.UserName))
                 .ForMember(x => x.Address, y => y.MapFrom(z => z.Address))
                 .ForMember(x => x.State, y => y.MapFrom(z => z.State))
                 .ForMember(x => x.Phone, y => y.MapFrom(z => z.Phone))
                 .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                 // todo: update to use enum
                
                 .ReverseMap();

            CreateMap<UpdateCompanyRequest, RegisterUser>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.State, y => y.MapFrom(z => z.State))
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.Phone))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
               .ReverseMap();
      
            
 
            #endregion

           
        }
    }

    internal class CompanyResponse
    {
    }
}