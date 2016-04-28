using System;
using System.Collections.Generic;
using System.Linq;
using JYSpaCinema.Domain;
using JYSpaCinema.Domain.Entities;

namespace JYSpaCinema.Service.DTO
{
    //[DataContract]
    public class PaginationResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        //[DataMember]
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 分页数据结果
        /// </summary>
        //[DataMember]
        public IReadOnlyList<T> Results { get; set; }

        //public PaginationResult(IPagedList<T> list)
        //{
        //    this.CurrentPageIndex = list.CurrentPageIndex;
        //    this.PageSize = list.PageSize;
        //    this.TotalItemCount = list.TotalItemCount;
        //    this.Results = list.ToList();
        //}

        public PaginationResult(int totalCount, IReadOnlyList<T> items)
        {
            this.TotalItemCount = totalCount;
            this.Results = items;
        }
    }
}