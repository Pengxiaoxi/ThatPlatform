namespace Tpf.Common.Options
{
    public class DatabaseOptions : BaseOptions
    {
        public override string SectionName => "Database";

        /// <summary>
        /// 主数据库
        /// </summary>
        public string? Main { get; set; }

        /// <summary>
        /// ORM
        /// </summary>
        public ORMOption? ORM { get; set; }
    }

    public class ORMOption
    {
        /// <summary>
        /// IBaseReposiotry<> 默认ORM
        /// </summary>
        public string? Main { get; set; }

        /// <summary>
        /// 从属使用ORM
        /// </summary>
        public List<string>? Slaves { get; set; }

    }
}
