using InsuranceSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Domain.Expenses
{
    public class Expense : BaseEntity
    {
        public string ExpenseType { get; set; } // Procedure or Prescription
        public string ExpenseDescription { get; set; }	// Description (name of procedure or name of medication)
    }
}
