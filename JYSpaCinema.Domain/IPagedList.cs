using System.Collections;
using System.Collections.Generic;

namespace JYSpaCinema.Domain
{
    public interface IPagedList : IEnumerable
    {
        /// <summary>
        /// 当前页号（从1开始）
        /// </summary>
        int CurrentPageIndex { get; set; }

        /// <summary>
        /// 分页容量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalItemCount { get; set; }
    }

    public interface IPagedList<T> : IEnumerable<T>, IPagedList { }
}
