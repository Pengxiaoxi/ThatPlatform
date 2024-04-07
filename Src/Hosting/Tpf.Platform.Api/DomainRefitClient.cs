using Microsoft.Extensions.DependencyInjection;
using Refit;
using Tpf.Authentication;
using Tpf.Common.ConfigOptions;
using Tpf.Domain.Common.RestApplication;
using Tpf.Middlewares.Refit;
using Tpf.Utils;

namespace Tpf.Platform.Api
{
    /// <summary>
    /// DomainRefitClient
    /// </summary>
    public static class DomainRefitClient
    {
        /// <summary>
        /// AddBaseInfoDomainRefitClient
        /// </summary>
        /// <param name="services"></param>
        public static void AddBaseInfoDomainRefitClient(this IServiceCollection services)
        {
            var refitApiOptions = ConfigHelper.GetOptions<RefitApisOptions>(RefitApisOptions.Name);

            services.AddRefitClient<IAuthRestSerivce>(x => RefitSettingsExtensions.GetRefitSettings())
                .ConfigureHttpClient(x =>
                {
                    x.BaseAddress = new Uri(refitApiOptions.AuthService);
                    x.AddHttpHeader_Authorization();
                });


        }
    }
}
