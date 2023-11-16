using InsuranceSystem.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Infrastructure.Dto
{
    public class ClaimsDto
    {
        [Required(ErrorMessage = "National Id is required")]
        [MaxLength(30, ErrorMessage = "Maximum length exceeded")]
        [MinLength(3, ErrorMessage = "Invalid ID number")]
        public string NationalIDOfPolicyHolder { get; set; }

        public int ExpenseId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateOfExpense { get; set; }

        public string ClaimsId { get; set; }
    }
}
