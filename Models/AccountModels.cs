using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using MvcSimpleMusicSite_CourseProject.Helpers;
using MvcSimpleMusicSite_CourseProject.Resources.Models;

namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class LogOnModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountRes))]
        [LocalizedDisplayName("YourLogin", NameResourceType = typeof(NamesAtrRes))]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль 
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountRes))]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("YourPassword", NameResourceType=typeof(NamesAtrRes))]
        public string Password { get; set; }

        /// <summary>
        /// Записать в куки или нет
        /// </summary>
        [LocalizedDisplayName("RememberMe", NameResourceType=typeof(NamesAtrRes))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        /// <summary>
        /// Имя для регистрации
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountRes))]
        [LocalizedDisplayName("YourLogin", NameResourceType=typeof(NamesAtrRes))]
        public string UserName { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountRes))]
        [DataType(DataType.EmailAddress)]
        [LocalizedDisplayName("YourEmail", NameResourceType=typeof(NamesAtrRes))]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AccountRes))]
        [StringLength(100, ErrorMessageResourceName = "PasswordMinLenght", ErrorMessageResourceType = typeof(AccountRes), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("YourPassword", NameResourceType=typeof(NamesAtrRes))]
        public string Password { get; set; }

        /// <summary>
        /// Повтор пароля
        /// </summary>
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(NamesAtrRes))]
        [Compare("Password", ErrorMessageResourceName = "PasswordMustMath", ErrorMessageResourceType = typeof(AccountRes))]
        public string ConfirmPassword { get; set; }
    }
}
