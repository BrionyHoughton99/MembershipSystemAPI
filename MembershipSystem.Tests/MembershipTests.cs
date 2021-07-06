using MembershipSystem.Api.Controllers;
using MembershipSystem.Api.Services;
using MembershipSystem.Models.DTOs;
using MembershipSystem.Models.Models;
using MembershipSystem.Services;
using MembershipSystem.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MembershipSystem.Tests
{
    [TestClass]
    public class MembershipTests : BaseTests
    {
        [TestMethod]
        public async Task GetAllMembers()
        {
            //setting configurations for database instane and mapper coming from base tests class allowing for use in test methods
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            //adding data to the database instance 
            context.AccountMembers.Add(new AccountMember()
            {
                Name = "Briony Houghton",
                Email = "briony.houghton@prodo.com",
                CardNumber = "F5MM24z97XbQKwVH",
                MobileNumber = "075681882819",
                Pin = "1234"
            });
            context.AccountMembers.Add(new AccountMember()
            {
                Name = "Retania Welch",
                Email = "retania.welch@prodo.com",
                CardNumber = "fsMfhYr6XdcVPT8P",
                MobileNumber = "077895638723",
                Pin = "1111"
            });
            context.SaveChanges();
            var context2 = BuildContext(databaseName);

            var controller = new AccountMemberController(context2, mapper, null, null);
            var response = await controller.Get();

            var allMembers = response.Value;
            Assert.AreEqual(2, allMembers.Count());
        }

        [TestMethod]
        public void GetAccountMemberById()
        {
            //initializing properties to use for test
            var cardNumber = "fsMfhYr6XdcVPT8P";
            var accountMemberDTO = new AccountMemberDTO()
            {
                Name = "Briony Houghton",
                CardNumber = cardNumber
            };
            //creating an instance of account service class to access GetAccountMemberById
            var mock = new Mock<IAccountService>();
            mock.Setup(x => x.GetAccountMemberById(cardNumber)).Returns(Task.FromResult(accountMemberDTO));

            //checking that the cardnumber set is same as cardnumber in database
            Assert.AreEqual(cardNumber, accountMemberDTO.CardNumber);
        }

        [TestMethod]
        public async Task CreateAccountMember()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();
            var memberName = "Briony Houghton";
            var createNewMember = new CreateAccountMemberDTO()
            {
                Name = memberName,
                Email = "briony.houghton@prodo.com",
                MobileNumber = "075637328912",
                CardNumber = "fsMfhYr6XdcVPT8P",
                Pin = "1111",
                LoginSuccessful = true,
                Success = true
            };
            context.SaveChanges();
            var context2 = BuildContext(databaseName);

            var mock = new Mock<IAccountService>();
            mock.Setup(x => x.CreateNewMember(createNewMember)).Returns(true);

            var controller = new AccountMemberController(context2, mapper, mock.Object, null);
            var response = controller.Post(createNewMember);
            
            


            //checking that the member's name created is equal to the name saved to the database in service class
            Assert.AreEqual(1, createNewMember);

        }


        [TestMethod]
        public void TopUpAccount()
        {
            var cardNumber = "fsMfhYr6XdcVPT8P";
            var member = new TopUpDTO()
            {
                CardNumber = cardNumber,
                Pin = "1111",
                TopUpAmount = 10
            };
            var mock = new Mock<IAccountService>();
            mock.Setup(x => x.TopUpAccount(member)).Returns(true);
            Assert.AreEqual(cardNumber, member.CardNumber);

        }

        [TestMethod]
        public async Task DeleteMemberNotFound()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            var controller = new AccountMemberController(context, mapper, null, null);
            var response = await controller.Delete(1);
            var result = response as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteMember()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = BuildContext(databaseName);
            var mapper = BuildMap();

            //adding data to the database instance 
            context.AccountMembers.Add(new AccountMember()
            {
                Name = "Briony Houghton",
                Email = "briony.houghton@prodo.com",
                CardNumber = "F5MM24z97XbQKwVH",
                MobileNumber = "075681882819",
                Pin = "1234"
            });
            context.AccountMembers.Add(new AccountMember()
            {
                Name = "Retania Welch",
                Email = "retania.welch@prodo.com",
                CardNumber = "fsMfhYr6XdcVPT8P",
                MobileNumber = "077895638723",
                Pin = "1111"
            });
            context.SaveChanges();
            var context2 = BuildContext(databaseName);

            var controller = new AccountMemberController(context2, mapper, null, null);
            var response = await controller.Delete(1);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
