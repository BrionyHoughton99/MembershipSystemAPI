using System;
using System.Collections.Generic;
using System.Text;

namespace MembershipSystem.Models.Models
{
    public class MemberLogin
    {
        public bool IsRegistered { get; set; }

        public string Name { get; set; }

        public bool SessionStartRequired { get; set; }
        public bool LoginSuccessful { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Pin { get; set; }
        public string CardNumber { get; set; }

    }
}
