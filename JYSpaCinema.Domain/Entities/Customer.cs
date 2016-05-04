using System;
using System.Collections.Generic;

namespace JYSpaCinema.Domain.Entities
{
    public class Customer : IEntityBase<int>
    {
        public int ID { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// EMail地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 信用卡号
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        public string GetFullName()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }
    }
}
