using Tpf.Common.ConfigOptions;

namespace Tpf.Caching.CSRedisCore
{
    /// <summary>
    /// RedisOptions
    /// </summary>
    public class RedisOptions : BaseOptions
    {
        public override string SectionName => "Caching:Redis";

        /// <summary>
        /// 默认连接
        /// </summary>
        public string? Default { get; set; }


        // Cluser...
        public string[]? Clusers { get; set; }

    }
}
