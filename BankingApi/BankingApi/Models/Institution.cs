using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankingApi.Models
{
    public class Institution
    {
        public int InstitutionId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
