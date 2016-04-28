using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 电影信息
    /// </summary>
    public class Movie : IEntityBase<int>
    {
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string Image { get; set; }

        public int GenreId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual Genre Genre { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Writer { get; set; }

        /// <summary>
        /// 制片人
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public byte Rating { get; set; }

        /// <summary>
        /// 预告地址
        /// </summary>
        public string TrailerURI { get; set; }

        /// <summary>
        /// 库存列表
        /// </summary>
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
