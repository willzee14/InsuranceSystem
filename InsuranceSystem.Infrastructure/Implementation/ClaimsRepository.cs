using Dapper;
using InsuranceSystem.Infrastructure.Abstraction;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable
namespace InsuranceSystem.Infrastructure.Implementation
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly string conncetionString;
        private readonly IConfiguration _con;
        public ClaimsRepository(IConfiguration con)
        {
            _con = con;
            conncetionString = _con["ConnectionStrings:dbConnection"].ToString();
        }

        public async Task<int> InsetAudit(AuditTrail auditTrail)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Insert Into [dbo].[AuditTrail] (Action,ClientName,DateGenerated,IPAddress,Request,Response)
                                     Values(@Action,@ClientName,@DateGenerated,@IPAddress,@Request,@Response)";


                var parameters = new DynamicParameters();
                parameters.Add("Action", auditTrail.Action);
                parameters.Add("ClientName", auditTrail.ClientName);
                parameters.Add("DateGenerated", DateTime.Now);
                parameters.Add("IPAddress", auditTrail.IPAddress);
                parameters.Add("Request", auditTrail.Request);
                parameters.Add("Response", auditTrail.Response);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at insert audit: {ex}");
                return -1;
            }
        }
    }
}
