using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ParkyAPI.Configuration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;


        public void Configure(SwaggerGenOptions options)
        {
            _provider.ApiVersionDescriptions.ToList().ForEach(apiVersionDescription =>
            {
                options.SwaggerDoc(
                    apiVersionDescription.GroupName, new OpenApiInfo()
                    {
                        Title = $"Parky API{apiVersionDescription.ApiVersion}",
                        Version = apiVersionDescription.ApiVersion.ToString()
                    });
            });
        }
    }
}
