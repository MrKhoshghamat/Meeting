using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.IdentityServer;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Identity;

public static class IdentityConfiguration
{
    public static IServiceCollection ConfigureIdentityService(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryApiResources(IdentityServerConfig.GetApis())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryPersistedGrants()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();

        services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "http://localhost:5000";
                options.Audience = "myMeetingsAPI";
                options.RequireHttpsMetadata = false;
            });

        return services;
    }

    public static IApplicationBuilder AddIdentityService(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
        return app;
    }
}
