using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tpf.Utils;
using Tpf.Utils.Check;

namespace Tpf.Caching.CSRedisCore
{
    public static class CSRedisCoreExtenisons
    {
        /// <summary>
        /// CSRedisCore
        /// https://github.com/2881099/csredis
        /// IDistributedCache: https://github.com/2881099/csredis/blob/master/src/Microsoft.Extensions.Caching.CSRedis/README.md
        /// </summary>
        /// <param name="services"></param>
        public static void AddTpfCSRedisCore(this IHostApplicationBuilder builder)
        {
            var redisOptions = ConfigHelper.GetOptions<RedisOptions>();

            if (redisOptions is null)
            {
                return;
            }

            //Check.NotNull(redisOptions, nameof(redisOptions));

            CSRedisClient csredis = null;
            if (!string.IsNullOrWhiteSpace(redisOptions.Default))
            {
                csredis = new CSRedis.CSRedisClient(redisOptions.Default);
            }
            else if (redisOptions.Cluser != null && redisOptions.Cluser.Length > 0)
            {
                csredis = new CSRedis.CSRedisClient(null, redisOptions.Cluser);
            }

            Check.NotNull(csredis, nameof(csredis), "CSRedisClient not be null.");

            RedisHelper.Initialization(csredis);

            // IDistributedCache
            //builder.Services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));

        }
    }
}
