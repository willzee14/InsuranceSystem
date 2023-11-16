using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Infrastructure.Dto
{
    public class PolicyHolderDto
    {
        [Required(ErrorMessage = "National ID number is required")]
        [MaxLength(20, ErrorMessage = "Maximum length exceeded")]
        public string NationalIDNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Maximum length exceeded")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(30, ErrorMessage = "Maximum length exceeded")]
        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Policy number is required")]
        [MaxLength(30, ErrorMessage = "Maximum length exceeded")]
        public string PolicyNumber { get; set; }
    }
}
