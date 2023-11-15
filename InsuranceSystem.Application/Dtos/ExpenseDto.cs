using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ExpenseDto
    {
        public string Type { get; set; } 
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
