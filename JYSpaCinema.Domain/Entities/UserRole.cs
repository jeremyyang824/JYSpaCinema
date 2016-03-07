using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 人员角色关系
    /// </summary>
    public class UserRole : IEntityBase<int>
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
