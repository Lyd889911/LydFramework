

namespace LydFramework.EFCore.UnitOfWorks
{
    /// <summary>
    /// 需要使用EFCore自动工作单元的，就把该特性标注到控制器方法上
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EFCoreUnitOfWorkAttribute : Attribute
    {
    }
}
