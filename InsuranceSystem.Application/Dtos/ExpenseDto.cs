﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ExpenseDto
    {
        public string ExpenseType { get; set; } 
        public string ExpenseDescription { get; set; }
    }
}
