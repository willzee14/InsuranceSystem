using InsuranceSystem.Application.Enum;
using InsuranceSystem.Domain.Common;
using InsuranceSystem.Domain.Entities;
using InsuranceSystem.Domain.Expenses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Domain.Claims
{
    public class Claim : BaseEntity
    {
        public string ClaimsId { get; set; }

        public string NationalIDOfPolicyHolder { get; set; }

        [ForeignKey("Expenses")]
        public int ExpenseId { get; set; }
        public Expense Expenses { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateOfExpense { get; set; }
        public ClaimStatus Status { get; set; }
    }
}
