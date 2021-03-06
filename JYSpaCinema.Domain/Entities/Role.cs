﻿using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : IEntityBase<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            this.Users = new List<User>();
        }
    }
}
