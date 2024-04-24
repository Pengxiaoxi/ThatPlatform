using Tpf.Common.ConfigOptions;

namespace Tpf.BaseRepository
{
    public class DatabaseOptions : BaseOptions
    {
        public override string SectionName => "Database";

        /// <summary>
        /// 主数据库
        /// </summary>
        public string? Main { get; set; }

        /// <summary>
        /// ORMOptions
        /// </summary>
        public ORMOptions? ORMOptions { get; set; }

    }

    /// <summary>
    /// ORMOptions
    /// </summary>
    public class ORMOptions
    {
        /// <summary>
        /// 主ORM
        /// </summary>
        public string? Main { get; set; }

        /// <summary>
        /// 其他ORM（配置后自动注入，便于直接使用）
        /// </summary>
        [Obsolete("改配置不如改代码")]
        public string[]? Slaves { get; set; }
    }


}
