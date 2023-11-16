using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Dtos
{
    public class ServiceResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseData { get; set; }       

    }
}
