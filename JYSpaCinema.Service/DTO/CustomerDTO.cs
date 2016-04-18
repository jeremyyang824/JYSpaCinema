using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JYSpaCinema.Service.Validators;

namespace JYSpaCinema.Service.DTO
{
    public class CustomerDto : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CustomerDtoValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
