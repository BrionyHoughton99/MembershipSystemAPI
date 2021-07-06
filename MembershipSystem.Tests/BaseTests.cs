using AutoMapper;
using MembershipSystem.Api.Helpers;
using MembershipSystem.Api.Services;
using MembershipSystem.Database;
using MembershipSystem.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MembershipSystem.Tests
{
    public class BaseTests
    {
        protected ApplicationDbContext BuildContext(string databaseName)
        {
            //using .net entity in memory nuget package to serve in memory instances of a database to test methods 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper BuildMap()
        {
            //setting mapper configurations to use in test methods
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }

    }
}
