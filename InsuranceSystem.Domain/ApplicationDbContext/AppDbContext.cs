using InsuranceSystem.Domain.Claims;
using InsuranceSystem.Domain.Expenses;
using InsuranceSystem.Domain.Policy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Domain.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PolicyHolder> Policyholders { get; set; }
    }
}
