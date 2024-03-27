
namespace LydFramework.Application.DtoParams
{
    public class PageParam
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 检索值
        /// </summary>
        public string? SearchValue { get; set; }
    }
}
