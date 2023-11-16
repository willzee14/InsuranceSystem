using Dapper;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Infrastructure.Dto;
using InsuranceSystem.Infrastructure.Enum;
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
    public class PolicyHolderRepository : IPolicyHolderRepository
    {
        private readonly string conncetionString;
        private readonly IConfiguration _con;
        public PolicyHolderRepository(IConfiguration con)
        {
            _con = con;
            conncetionString = _con["ConnectionStrings:dbConnection"].ToString();
        }
        public async Task<PolicyHolderDto> GetByPolicyNumber(PolicyHolderDto policyHolderDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Policies] Where NationalIDNumber = @NationalIDNumber";

                var parameters = new DynamicParameters();
                parameters.Add("NationalIDNumber", policyHolderDto.NationalIDNumber);
                var result = await sqlConnection.QueryFirstOrDefaultAsync<PolicyHolderDto>(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at GetByPolicyNumber: {ex}");
                return null;
            }
        }

        public async Task<List<PolicyHolderDto>> GetPolicies()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Policies]";


                var result = await sqlConnection.QueryAsync<PolicyHolderDto>(query, commandType: CommandType.Text);

                return result.ToList();
            }
            catch (Exception ex)
            {
                Log.Error($"Error at GetPolicies: {ex}");
                return null;
            }
        }

        public async Task<int> InsetPolicy(PolicyHolderDto policyHolderDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Insert Into [dbo].[Policies] (NationalIDNumber,Name,Surname,DateOfBirth,PolicyNumber,DateCreated)
                                     Values(@NationalIDNumber,@Name,@Surname,@DateOfBirth,@PolicyNumber,@DateCreated)";

                var parameters = new DynamicParameters();
                parameters.Add("NationalIDNumber", policyHolderDto.NationalIDNumber);
                parameters.Add("Name", policyHolderDto.Name);
                parameters.Add("Surname", policyHolderDto.Surname);
                parameters.Add("DateOfBirth", policyHolderDto.DateOfBirth);
                parameters.Add("PolicyNumber", policyHolderDto.PolicyNumber);                
                parameters.Add("DateCreated", DateTime.UtcNow);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at InsetPolicy: {ex}");
                return -1;
            }
        }

        public async Task<int> UpdatePolicy(PolicyHolderDto policyHolderDto)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Update [dbo].[Policies] Set NationalIDNumber =@NationalIDNumber,Name =@Name,Surname =@Surname,DateOfBirth =@DateOfBirth,PolicyNumber =@PolicyNumber,DateModified =@DateModified";
                                     

                var parameters = new DynamicParameters();
                parameters.Add("NationalIDNumber", policyHolderDto.NationalIDNumber);
                parameters.Add("Name", policyHolderDto.Name);
                parameters.Add("Surname", policyHolderDto.Surname);
                parameters.Add("DateOfBirth", policyHolderDto.DateOfBirth);
                parameters.Add("PolicyNumber", policyHolderDto.PolicyNumber);
                parameters.Add("DateModified", DateTime.UtcNow);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at UpdatePolicy: {ex}");
                return -1;
            }
        }
    }
}
