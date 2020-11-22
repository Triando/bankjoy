using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankingApi.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [Required]
        public string GivenName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int InstitutionId { get; set; }
        public List<Account> Accounts { get; set; }       
    }
}
