using InsuranceSystem.Application.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Utility
{
    public static class RequestHandler
    {
        public static ServiceResponse SplitRequest(string req)
        {
            ServiceResponse res = new ServiceResponse();
            var splitRes = req?.Split('=');
            if (splitRes != null && splitRes[0].Equals("Unauthorized"))
            {
                res.ResponseCode = "03";
                res.ResponseMessage = "Unauthorized";
                return res;
            }

            var deserializeReq = JsonConvert.DeserializeObject<dynamic>(splitRes[^1]);
            if (deserializeReq == null)
            {
                var response = new ServiceResponse() { ResponseCode = "30", ResponseMessage = "invalid request" };
                return response;
            }

            return new ServiceResponse() { ResponseCode = "00", ResponseData = deserializeReq };
        }
    }
}
