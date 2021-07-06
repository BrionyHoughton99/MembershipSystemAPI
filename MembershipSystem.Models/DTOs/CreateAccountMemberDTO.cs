using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MembershipSystem.Models.DTOs
{
    public class CreateAccountMemberDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        public string CardNumber { get; set; }

        [MinLength(4, ErrorMessage = "Pin number must be 4 numbers")]
        [MaxLength(4, ErrorMessage = "Pin number must be 4 numbers")]
        [Required(ErrorMessage = "Pin number is required")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNumber { get; set; }
        public bool LoginSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
