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
        public static RequestHandlerDto<T> SplitRequest<T>(string req)
        {
            RequestHandlerDto<T> res = new RequestHandlerDto<T>();
            var splitRes = req?.Split('=');
            if (splitRes != null && splitRes[0].Equals("Unauthorized"))
            {
                res.ResponseCode = "03";
                res.ResponseMessage = "Unauthorized";
                //return res;
            }
            var valueee = splitRes[^1];
            try
            {
                var deserializeReq = JsonConvert.DeserializeObject<T>(valueee);
                if (deserializeReq == null)
                {
                    var response = new RequestHandlerDto<T>() { ResponseCode = "30", ResponseMessage = "invalid request" };
                    //return response;
                }
                return new RequestHandlerDto<T>() { ResponseCode = "00", ResponseData = deserializeReq }; 
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
