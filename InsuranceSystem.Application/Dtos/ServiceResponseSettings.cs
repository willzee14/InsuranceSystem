using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ServiceResponseSettings
    {
        public string SuccessCode { get; set; }
        public string SuccessMessage { get; set; }
        public string NotFoundCode { get; set; }
        public string NotFoundMessage { get; set; }
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
        public string ErrorOccuredCode { get; set; }
        public string ErrorOccuredMessage { get; set; }
    }
}
