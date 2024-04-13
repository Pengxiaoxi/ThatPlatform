using Microsoft.Extensions.DependencyInjection;
using Refit;
using Tpf.Authentication;
using Tpf.Common.ConfigOptions;
using Tpf.Domain.Common.RestApplication;
using Tpf.Middlewares.Refit;
using Tpf.Utils;

namespace Tpf.Domain.BaseInfo.HttpApi.RefitClient
{
    public static class DomainRefitClient
    {
        public static void AddBaseInfoDomainRefitClient(this IServiceCollection services)
        {
            var refitApiOptions = ConfigHelper.GetOptions<BaseInfoHttpApiOptions>();

            services.AddRefitClient<IAuthRestSerivce>(x => RefitSettingsExtensions.GetRefitSettings())
                .ConfigureHttpClient(x => 
                {
                    x.BaseAddress = new Uri(refitApiOptions.AuthService);
                    x.AddHttpHeader_Authorization();
                });


        }
    }
}
