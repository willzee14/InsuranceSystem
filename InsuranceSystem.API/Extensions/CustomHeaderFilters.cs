using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InsuranceSystem.API.Extensions
{
    public class CustomHeaderFilters
    {
        public class AddRequiredHeaderParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "authorization",
                    In = ParameterLocation.Header,
                    Required = true,
                    Description = "Client Identifier (Required)"
                });

            }
        }
    }
}
