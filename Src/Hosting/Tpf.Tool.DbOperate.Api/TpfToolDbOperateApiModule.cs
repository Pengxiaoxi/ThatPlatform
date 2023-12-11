using Tpf.Common.CommonAttributes;
using Tpf.Common.Module;

namespace Tpf.Tool.DbOperate.Api
{
    /// <summary>
    /// TpfCoreApiModule
    /// </summary>
    [DependsOn(
        //typeof(AuthInfoHttpApiModule)
        )]
    public class TpfToolDbOperateApiModule : AppModule
    {
    }
}
