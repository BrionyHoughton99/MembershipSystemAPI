using AutoMapper;
using MembershipSystem.Models.DTOs;
using MembershipSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipSystem.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //using auto mapper to map the properties set in application layer to the data transfer object
            CreateMap<AccountMember, AccountMemberDTO>().ReverseMap();
            CreateMap<AccountMember, CreateAccountMemberDTO>().ReverseMap();
            CreateMap<Session, AccountMemberDTO>().ReverseMap();
        }
    }
}
