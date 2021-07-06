using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MembershipSystem.Models.Models
{
    public class Session
    {
        [Key]
        public string Name { get; set; }
        public bool IsRegistered { get; set; }
        public string CardNumber { get; set; }
        public string LogoutMessage { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public bool EndSession { get; set; }

    }
}
