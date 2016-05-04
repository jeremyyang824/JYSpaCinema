using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 租借信息
    /// </summary>
    public class Rental : IEntityBase<int>
    {
        public int ID { get; set; }

        public int CustomerId { get; set; }

        /// <summary>
        /// 顾客信息
        /// </summary>
        public virtual Customer Customer { get; set; }

        public int StockId { get; set; }
        /// <summary>
        /// 库存信息
        /// </summary>
        public virtual Stock Stock { get; set; }

        /// <summary>
        /// 借出日期
        /// </summary>
        public DateTime RetalDate { get; set; }

        /// <summary>
        /// 归还日期
        /// </summary>
        public DateTime? ReturnedDate { get; set; }

        /// <summary>
        /// 借还状态
        /// </summary>
        public string Status { get; set; }
    }
}
