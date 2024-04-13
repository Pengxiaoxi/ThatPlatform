using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Tpf.Middlewares.Newtonsoft
{
    public static class NewtonsoftMiddlewareExtensions
    {
        public static void AddNewtonsoftJsonMiddleware(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                //将long类型转为string
                //options.SerializerSettings.Converters.Add(new NumberConverter(NumberConverterShip.Int64));
            });
        }
    }
}
