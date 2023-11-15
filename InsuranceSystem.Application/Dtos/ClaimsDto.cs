using InsuranceSystem.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ClaimsDto
    {
        public string PolicyholderNationalId { get; set; }
        public List<ExpenseDto> Expenses { get; set; }
        public DateTime DateOfExpense { get; set; }
        public ClaimStatus Status { get; set; }
    }
}
