using System.Collections.Generic;
using System.Linq;
using JYSpaCinema.Domain;

namespace JYSpaCinema.Service.DTO
{
    //[DataContract]
    public class PaginationResult<T>
    {
        /// <summary>
        /// 当前页号（从1开始）
        /// </summary>
        //[DataMember]
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 分页容量
        /// </summary>
        //[DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        //[DataMember]
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 分页数据结果
        /// </summary>
        //[DataMember]
        public List<T> Results { get; set; }

        public PaginationResult()
        {
        }

        public PaginationResult(IPagedList<T> list)
        {
            this.CurrentPageIndex = list.CurrentPageIndex;
            this.PageSize = list.PageSize;
            this.TotalItemCount = list.TotalItemCount;
            this.Results = list.ToList();
        }
    }
}