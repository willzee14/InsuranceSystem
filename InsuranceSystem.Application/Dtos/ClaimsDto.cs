using InsuranceSystem.Application.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ClaimsDto
    {
        public string ClaimsId { get; set; }
        public string NationalIDOfPolicyHolder { get; set; }        
        public int ExpenseId { get; set; }       
        public decimal Amount { get; set; }
        public string DateOfExpense { get; set; }
        //public ClaimStatus ClaimStatus { get; set; }
    }
}
