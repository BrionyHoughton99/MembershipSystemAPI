using System;
using System.Collections.Generic;
using System.Text;

namespace MembershipSystem.Models.DTOs
{
    public class AccountMemberDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }

    }
}
