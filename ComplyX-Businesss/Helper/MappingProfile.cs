using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Graph.Models.Security;
using ComplyX.Repositories.Repositories.Abstractions; // Replace with your Destination class's namespace

namespace ComplyX_Businesss.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, IUserRespositories>();
        }
    }
}
