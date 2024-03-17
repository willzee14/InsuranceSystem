using Dapper;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Infrastructure.Dto;
using InsuranceSystem.Infrastructure.Dtos;
using InsuranceSystem.Infrastructure.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public async Task<List<ClaimsDto>> GetAllClaims()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Claims]";
                               

                var result = await sqlConnection.QueryAsync<ClaimsDto>(query,commandType: CommandType.Text);

                return result.ToList();
            }
            catch (Exception ex)
            {
                Log.Error($"Error at GetAllClaims: {ex}");
                return null;
            }
        }

        public async Task<int> InsetClaim(ClaimsDto claimsDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Insert Into [dbo].[Claims] (ClaimsId,NationalIDOfPolicyHolder,ExpenseId,Amount,DateOfExpense,ClaimStatus,DateCreated,DateModified)
                                     Values(@ClaimsId,@NationalIDOfPolicyHolder,@ExpenseId,@Amount,@DateOfExpense,@ClaimStatus,@DateCreated,@DateModified)";


                var parameters = new DynamicParameters();
                parameters.Add("ClaimsId", DateTime.Now.Ticks.ToString());
                parameters.Add("NationalIDOfPolicyHolder", claimsDto.NationalIDOfPolicyHolder);
                parameters.Add("ExpenseId", claimsDto.ExpenseId);
                parameters.Add("Amount", claimsDto.Amount);
                parameters.Add("DateOfExpense", claimsDto.DateOfExpense);
                parameters.Add("ClaimStatus", ClaimStatus.Submitted);
                parameters.Add("DateCreated", DateTime.UtcNow);
                parameters.Add("DateModified", DateTime.UtcNow);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at InsetClaim: {ex}");
                return -1;
            }
        }

        public async Task<int> UpdateClaim(ClaimsDto claimsDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Update [dbo].[Claims] Set ClaimsId = @ClaimsId,NationalIDOfPolicyHolder = @NationalIDOfPolicyHolder,ExpenseId = @ExpenseId,Amount =@Amount,DateOfExpense =@DateOfExpense,ClaimStatus =@ClaimStatus,DateModified =@DateModified Where ClaimsId = @ClaimsId";
                                    

                var parameters = new DynamicParameters();
                
                parameters.Add("ClaimsId", claimsDto.ClaimsId);
                parameters.Add("NationalIDOfPolicyHolder", claimsDto.NationalIDOfPolicyHolder);
                parameters.Add("ExpenseId", claimsDto.ExpenseId);
                parameters.Add("Amount", claimsDto.Amount);
                parameters.Add("DateOfExpense", claimsDto.DateOfExpense);
                parameters.Add("ClaimStatus", ClaimStatus.InReview);
                parameters.Add("DateCreated", DateTime.UtcNow);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at UpdateClaim: {ex}");
                return -1;
            }
        }

        public async Task<ClaimsDto> GetClaimsByNationalID(ClaimsDto claimsDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Claims] Where NationalIDOfPolicyHolder = @NationalIDOfPolicyHolder";

                var parameters = new DynamicParameters();
                parameters.Add("NationalIDOfPolicyHolder", claimsDto.NationalIDOfPolicyHolder);
                var result = await sqlConnection.QueryFirstOrDefaultAsync<ClaimsDto>(query, parameters,commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at GetClaimsByNationalID: {ex}");
                return null;
            }
        }
    }
}
