using System.ComponentModel;
using System.Threading.Tasks;
using Command.SettingCommand;
using Command.SmsSettingCommand;
using DataTransfer.EmailSettingDtos;
using DataTransfer.Setting;
using DataTransfer.SMSSettingDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.SettingControllers
{
    [DisplayName("تنظیمات سایت")]
    public class SettingController : BaseController , IPermissionMarker
    {
        private readonly IMediator mediator;

        public SettingController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut]
        [DisplayName("تنظیمات ایمیل")]
        public async Task<IActionResult> SetEmailSetting(AddEmailSetting emailSetting)
        {
            if (ModelState.IsValid)
            {
                var result = await mediator.Send(new CreateEmailSettingCommand
                {
                    From = emailSetting.From,
                    Password = emailSetting.Password,
                    Port = emailSetting.Port,
                    SmtpServer = emailSetting.SmtpServer,
                    Username = emailSetting.Username
                }
                );
                if (result.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [DisplayName("تنظیمات پیامک")]
        public async Task<IActionResult> SetSMSSetting(AddSMSSetting smsSetting)
        {
            var result = await mediator.Send(new SetSmsSettingCommand
            {
                LineNumber = smsSetting.LineNumber,
                secretKey = smsSetting.secretKey,
                userApikey = smsSetting.userApikey
            });
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("تنظیمات کاربران هنگام ثبت نام")]
        public async Task<IActionResult> SetRegisterUserSetting(RegisterUserSetting registerUserSetting)
        {
            var result = await mediator.Send(new SetRegisterUserSettingCommand
            {
                RegisterRoleByAdmin = registerUserSetting.RegisterRoleByAdmin,
                RegisterRoleByUser = registerUserSetting.RegisterRoleByUser,
                SendCodeVerification = registerUserSetting.SendCodeVerification
            });
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}