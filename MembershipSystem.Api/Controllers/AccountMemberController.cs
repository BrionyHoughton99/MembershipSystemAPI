using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MembershipSystem.Api.Services;
using MembershipSystem.Database;
using MembershipSystem.Models.DTOs;
using MembershipSystem.Models.Models;
using MembershipSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MembershipSystem.Api.Controllers
{
    [Route("api/membership")]
    [ApiController]
    public class AccountMemberController : ControllerBase
    {

        #region Private Properties
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;

        #endregion

        #region Constructors
        public AccountMemberController(
            ApplicationDbContext context,
            IMapper mapper,
            IAccountService _accountService,
            ISessionService _sessionService)
        {
            this.context = context;
            this.mapper = mapper;
            this._accountService = _accountService;
            this._sessionService = _sessionService;
        }
        #endregion

        //this sets HttpGet sets the route api/membership
        [HttpGet]
        public async Task<ActionResult<List<AccountMemberDTO>>> Get()
        {
            var accountMembers = await context.AccountMembers.ToListAsync();
            //mapping properties to its DTO
            var accountMemberDTO = mapper.Map<List<AccountMemberDTO>>(accountMembers);

            return accountMemberDTO;
        }

        [HttpGet("{CardNumber}")]
        public async Task<ActionResult<Session>> Get(string cardNumber)
        {

            var registeredMember = await _accountService.GetAccountMemberById(cardNumber);
            var accountMember = mapper.Map<Session>(registeredMember);
            
            if (accountMember == null)
            {
                return new Session()
                {
                    //setting success to false to indicate that the user is not registered and can take them to a register screen
                    Name = accountMember.Name,
                    IsRegistered = false,
                    CardNumber = cardNumber,
                    Success = false
                };
            }
            else
            {
                var isLoggedIn = _sessionService.IsSessionActive();

                if (isLoggedIn)
                {
                    //if member is logged in this is enabling them to log out by second tap
                    _sessionService.EndSession();
                    return new Session()
                    {
                        Name = accountMember.Name,
                        IsRegistered = true,
                        CardNumber = accountMember.CardNumber,
                        LogoutMessage = "Goodbye",
                        EndSession = true
                    };
                }
                else
                {
                    //if member is registered but not logged in returns name on frontend and can try again
                    return new Session()
                    {
                        Name = accountMember.Name,
                        IsRegistered = true,
                        CardNumber = accountMember.CardNumber,
                        Success = true
                    };
                }

            }
        }
    
        [HttpPost]
        public CreateAccountMemberDTO Post([FromBody] CreateAccountMemberDTO createAccountMember)
        {
            if (!ModelState.IsValid)
            {
                return new CreateAccountMemberDTO()
                {
                    LoginSuccessful = false,
                    ErrorMessage = "Please check the details you have entered and ensure they meet all validation",
                    Success = false,
                };
            }
            else
            {
                var memberCreated = _accountService.CreateNewMember(createAccountMember);
                if (memberCreated)
                {
                    //if member created is true this sets the session
                    _sessionService.StartSession(createAccountMember.CardNumber);
                }
                return new CreateAccountMemberDTO()
                {
                    //setting properties to display in body response when HTTP Post new member
                    Name = createAccountMember.Name,
                    Email = createAccountMember.Email,
                    MobileNumber = createAccountMember.MobileNumber,
                    CardNumber = createAccountMember.CardNumber,
                    Pin = createAccountMember.Pin,
                    LoginSuccessful = true,
                    Success = true
                };
            }
        }

        [HttpPost("topup")]
        public TopUpDTO TopUpAccount(TopUpDTO topUpAccount)
        {
            if (!ModelState.IsValid)
            {
                return new TopUpDTO()
                {
                    TopUpSuccessful = false,
                    ErrorMessage = "Make sure details provided are correct"
                };
            }
            else
            {
                //if details are correct it will go to service class to look for existing balance and add top up amount to it 
                var topUpAccountSuccess = _accountService.TopUpAccount(topUpAccount);
                return new TopUpDTO()
                {
                    CardNumber = topUpAccount.CardNumber,
                    Pin = topUpAccount.Pin,
                    TopUpAmount = topUpAccount.TopUpAmount,
                    TopUpSuccessful = topUpAccountSuccess,
                    Success = topUpAccountSuccess
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //delete member based upon id 
            //checks to see if the member exists
            var accountMember = await context.AccountMembers.AnyAsync(x => x.Id == id);
            if (!accountMember)
            {
                //if does not exist return 404 no content
                return NotFound();
            }
            //this deletes the data
            context.Remove(new AccountMember() { Id = id });
            //saves the changes to the db
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
