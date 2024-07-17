namespace Tpf.Common.Options
{
    public class AppOptions : BaseOptions
    {
        public override string SectionName => "App";


        public string? Security16 { get; set; }

        public string? Security32 { get; set; }


    }
}
