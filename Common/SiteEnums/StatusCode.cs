using Localization.Resources.Controllers.V1.RoleControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.SiteEnums
{
    public enum StatusCode
    {
        [Display(Name = RoleControllerShared.AddSuccessRole)]
        Success = 0,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 1,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Display(Name = "یافت نشد")]
        NotFound = 3,

        [Display(Name = "لیست خالی است")]
        ListEmpty = 4,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,

        [Display(Name = "احراز هویت انجام نشده")]
        UnAuthorize = 6,

        [Display(Name = "شما اجازه دسترسی به این قسمت را ندارید")]
        UnAccess = 7
    }
}
