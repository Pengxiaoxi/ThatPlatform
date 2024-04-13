namespace Tpf.Common.ConfigOptions
{
    public class HttpApisOptions : BaseOptions
    {
        public const string Name = "HttpApis";

        public override string SectionName => "HttpApis";

    }

    /// <summary>
    /// Baseinfo api
    /// </summary>
    public class BaseInfoHttpApiOptions : HttpApisOptions
    {
        /// <summary>
        /// AuthService Url
        /// </summary>
        public string? AuthService { get; set; }

    }
}
