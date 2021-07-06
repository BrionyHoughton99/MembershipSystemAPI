using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MembershipSystem.Models.Models
{
    public class AccountMember
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Pin { get; set; }
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}
