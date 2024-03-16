using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Cryptography;
using System.Text;
using InsuranceSystem.Application.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using InsuranceSystem.Infrastructure.Abstraction;
#pragma warning disable

namespace InsuranceSystem.API.Extensions
{
    public class EncryptionActionFilter : IActionFilter
    {
        private ILogger<EncryptionActionFilter> logger;
        private string _request;
        private string _encryptedrequest;
        private readonly IClaimsRepository _dataAccessLayer;
        private readonly AppSettings _settings;
        public EncryptionActionFilter(ILogger<EncryptionActionFilter> logger, IClaimsRepository dataAccessLayer, IOptions<AppSettings> settings)
        {
            this.logger = logger;
            _request = "";
            _encryptedrequest = "";
            _dataAccessLayer = dataAccessLayer;
            _settings = settings.Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var data = context.HttpContext.Response.Body;
            var result = context.Result;

            string Key = string.Empty;

            var usernameValue = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            var authTokenCombo = _settings.Username + ":&" + _settings.Password;

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                             .GetBytes(authTokenCombo));

            var Authorization = "Basic" + encoded;

            if (!string.IsNullOrEmpty(usernameValue))
            {

                Key = _settings.ClientKey;

                if (result is JsonResult json)
                {
                    var x = json.Value;
                    var status = json.StatusCode;
                    var encryptdata = EncryptString(Key, JsonConvert.SerializeObject(x));
                    this.logger.LogInformation(JsonConvert.SerializeObject(x));
                }
                if (result is ObjectResult view)
                {

                    var status = view.StatusCode;
                    var x = view.Value;
                    
                    var reslt = JsonConvert.SerializeObject(x, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() } });
                    var encryptdata = EncryptString(Key, reslt);
                    view.Value = encryptdata;

                    var audit = new InsuranceSystem.Infrastructure.Dtos.AuditTrail
                    {
                        Action = context.HttpContext.Request.Path,
                        ClientName = Environment.MachineName,
                        IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        DateGenerated = DateTime.Now,
                        Request = _encryptedrequest,
                        Response = reslt
                    };
                    var insertAudit = _dataAccessLayer.InsetAudit(audit);
                    Log.Information($"response from insert audit: {insertAudit}");
                    this.logger.LogInformation(JsonConvert.SerializeObject(x));
                }


            }

        }
        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{responseBody}";
        }

        public async Task ProcessEncrytedBody(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;

            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EncryptClass);

            var requestBody = param.Value as EncryptClass;
            if (!string.IsNullOrWhiteSpace(requestBody.Data))
            {

                string Key = string.Empty;
                var usernameValue = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                var authTokenCombo = _settings.Username + ":" + _settings.Password;

                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                 .GetBytes(authTokenCombo));
                var Authorization = "Basic " + encoded;
                if (!string.IsNullOrWhiteSpace(usernameValue))
                {
                    if (Authorization.Equals(usernameValue))
                    {

                        Key = _settings.ClientKey;

                        var data = context.ActionArguments.FirstOrDefault();

                        var decryptRequest = DecryptString(Key, requestBody.Data);

                        _encryptedrequest = requestBody.Data;
                        _request = decryptRequest;                        
                        context.HttpContext.Items["data"] = decryptRequest;
                    }
                    else
                    {
                        var error = "Unauthorized";
                        context.HttpContext.Items["data"] = error;
                    }
                }
                else
                {
                    var error = "Unauthorized";
                    context.HttpContext.Items["data"] = error;
                }

            }
        }



        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{requestBody}";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;
            var data = context.HttpContext.Response.Body;
            var result = context.Result;
            if (!string.IsNullOrWhiteSpace(httpContext.Request.Path.Value))
            {
                try
                {
                    ProcessEncrytedBody(context).GetAwaiter().GetResult();
                    return;
                }
                catch (Exception)
                {

                    if (context.ActionArguments.Count > 0)
                    {

                        string Key = string.Empty;

                        var usernameValue = httpContext.Request.Headers["authorization"].FirstOrDefault();

                        var authTokenCombo = _settings.Username + ":&" + _settings.Password;

                        string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                         .GetBytes(authTokenCombo));

                        var Authorization = "Basic" + encoded;

                        if (!string.IsNullOrWhiteSpace(usernameValue))
                        {
                            if (Authorization.Equals(usernameValue))
                            {
                                Key = _settings.ClientKey;
                                var data1 = context.ActionArguments.LastOrDefault();
                               
                                var splitResult = data1.Value.ToString();
                                var split = string.Empty;

                                var decryptRequest = DecryptString(Key, splitResult);
                                string decryptedString = splitResult[0] + "=" + decryptRequest;

                                _encryptedrequest = splitResult;
                                _request = decryptRequest;
                                context.HttpContext.Items["data"] = decryptedString;

                            }
                            else
                            {
                                var error = "Unauthorized";
                                context.HttpContext.Items["data"] = error;
                            }
                        }
                        else
                        {
                            var error = "Unauthorized";
                            context.HttpContext.Items["data"] = error;
                        }

                    }
                }

            }

        }


        public static string DecryptString(string key, string cipherText)
        {

            byte[] iv = new byte[16];

            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(cipherText);
            }
            catch (Exception)
            {
                return cipherText;
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
              .Where(x => x % 2 == 0)
              .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
              .ToArray();
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            Console.WriteLine("IV is:", iv);
            byte[] array;
            Console.WriteLine(Convert.ToBase64String(iv));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                            streamWriter.Flush();
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

    }
}
