using AutoMapper;
using MembershipSystem.Database;
using MembershipSystem.Models.Models;
using MembershipSystem.Models.DTOs;
using MembershipSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MembershipSystem.Api.Services
{
    public class AccountService : IAccountService
    {
        #region Private Properties
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        #endregion

        #region Constructors
        public AccountService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        public virtual async Task<AccountMemberDTO> GetAccountMemberById(string cardNumber)
        {
            //accessing database to look for entry that has cardnumber entered into the route
            var accountMember = await context.AccountMembers.FirstOrDefaultAsync(x => x.CardNumber.ToLower() == cardNumber);

            if (accountMember != null)
            {
                //if member comes back it will set the properties to what was found to the model
                var accountDetails = new AccountMemberDTO()
                {
                    Name = accountMember.Name,
                    CardNumber = accountMember.CardNumber
                };
            }
            
            var accountMemberDTO = mapper.Map<AccountMemberDTO>(accountMember);
            return accountMemberDTO;
        }

        public virtual bool CreateNewMember(CreateAccountMemberDTO member)
        {
            //setting a bool value to specify if a member has been created using CreateAccountMemberDTO
            var memberCreated = false;
            try
            {
                var accountMember = mapper.Map<AccountMember>(member);
                context.Add(accountMember);
                context.SaveChanges();
                memberCreated = true;
            } catch
            {
                throw new ApplicationException();
            }
            return memberCreated;
        }


        public virtual bool TopUpAccount(TopUpDTO topUpAccount)
        {
            var topUpSuccess = false;
            try
            {
                //checking that the card number and pin are correct 
                var accountMember = context.AccountMembers.Where(x => x.CardNumber == topUpAccount.CardNumber && x.Pin == topUpAccount.Pin).FirstOrDefault();
                if(accountMember != null)
                {
                    //if member comes back then this logic looks at existing balance to add new top up amount to it
                    accountMember.Balance = accountMember.Balance + topUpAccount.TopUpAmount;
                    topUpSuccess = true;
                }
                context.SaveChanges();
            } catch
            {
                throw new ApplicationException();
            }
            return topUpSuccess;
        }
    }
}
