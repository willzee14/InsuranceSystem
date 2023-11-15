using InsuranceSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Domain.Policy
{
    public class PolicyHolder : BaseEntity
    {
        public string NationalIDNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PolicyNumber { get; set; }
    }
}
