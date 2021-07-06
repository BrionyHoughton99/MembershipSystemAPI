using MembershipSystem.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MembershipSystem.Database
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors
        //DbContextOptions contains configuration to sql server
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        #endregion
        //setting db table getting 
        public DbSet<AccountMember> AccountMembers { get; set; }
    }
}
