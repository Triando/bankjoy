using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingApi.Models
{
    public class InstitutionContext : DbContext
    {
        public InstitutionContext(DbContextOptions<InstitutionContext> options) : base(options)
        {

        }

        public DbSet<Institution> Institutions { get; set; }
    }
}
