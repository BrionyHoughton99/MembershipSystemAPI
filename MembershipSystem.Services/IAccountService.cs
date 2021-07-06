using MembershipSystem.Models.DTOs;
using MembershipSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MembershipSystem.Services
{
    public interface IAccountService
    {
        bool CreateNewMember(CreateAccountMemberDTO member);
        Task<AccountMemberDTO> GetAccountMemberById(string CardNumber);
        bool TopUpAccount(TopUpDTO topUpAccount);
    }
}
