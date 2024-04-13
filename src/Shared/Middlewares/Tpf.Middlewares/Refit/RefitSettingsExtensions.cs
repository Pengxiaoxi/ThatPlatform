using Refit;

namespace Tpf.Middlewares.Refit
{
    public class RefitSettingsExtensions
    {
        /// <summary>
        /// GetRefitSettings
        /// https://reactiveui.github.io/refit/#json-content
        /// </summary>
        /// <returns></returns>
        public static RefitSettings GetRefitSettings()
        {
            return new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(),
            };
        }
    }
}
