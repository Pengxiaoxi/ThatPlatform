using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tpf.Security;
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

            if (redisOptions is null || redisOptions.Enable == false)
            {
                return;
            }

            //Check.NotNull(redisOptions, nameof(redisOptions));

            CSRedisClient csredis = null;
            if (!string.IsNullOrWhiteSpace(redisOptions.Default))
            {
                var defaultConn = AESHelper.Decrypt(redisOptions.Default, ConfigHelper.GetSecurityKey16());

                csredis = new CSRedis.CSRedisClient(defaultConn);
            }
            else if (redisOptions.Clusers != null && redisOptions.Clusers.Length > 0)
            {
                var cluserConns = redisOptions.Clusers.Select(x => AESHelper.Decrypt(x, ConfigHelper.GetSecurityKey16())).ToArray();

                csredis = new CSRedis.CSRedisClient(null, cluserConns);
            }

            Check.NotNull(csredis, nameof(csredis), "CSRedisClient not be null.");

            RedisHelper.Initialization(csredis);

            // IDistributedCache
            //builder.Services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));

        }
    }
}
