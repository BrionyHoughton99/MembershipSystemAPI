using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MembershipSystem.Models.DTOs
{
    public class TopUpDTO
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string Pin { get; set; }
        public decimal TopUpAmount { get; set; }
        public bool TopUpSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
