using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project_NZWalks.API
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach(var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(apiVersionDescription.GroupName, CreateVersionInfo(apiVersionDescription));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Your Desired Version",
                Version = description.ApiVersion.ToString()
            };

            return info;
        }
    }
}
