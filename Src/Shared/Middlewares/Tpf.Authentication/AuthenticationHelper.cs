using Microsoft.Net.Http.Headers;
using Tpf.Domain.Base.Domain.Context;

namespace Tpf.Authentication
{
    public static class AuthenticationHelper
    {
        public static void AddHttpHeader_Authorization(this HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(HeaderNames.Authorization, UserContext.Token);

        }


    }
}
