using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 库存单品SKU
    /// </summary>
    public class Stock : IEntityBase<int>
    {
        public int ID { get; set; }

        public int MovieId { get; set; }
        /// <summary>
        /// 影片信息
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// SKU标识
        /// </summary>
        public Guid UniqueKey { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
