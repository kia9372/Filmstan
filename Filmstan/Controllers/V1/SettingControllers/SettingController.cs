using System.ComponentModel;
using System.Threading.Tasks;
using Command.SettingCommand;
using Command.SmsSettingCommand;
using Common;
using DataTransfer.EmailSettingDtos;
using DataTransfer.Setting;
using DataTransfer.SMSSettingDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.SettingQueries;
using Travel.Framework.Base;

namespace Command.Controllers.V1.SettingControllers
{
    [DisplayName("تنظیمات سایت")]
    public class SettingController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;

        public SettingController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSmsSetting()
        {
            var result = await mediator.Send(new SmsSettingQuery());
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailetting()
        {
            var result = await mediator.Send(new EmailSettingQuery());
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetRegisterUserSetting()
        {
            var result = await mediator.Send(new RegisterUserSettingQuery());
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
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
        public async Task<IActionResult> SetSMSSetting([FromBody]AddSMSSetting smsSetting)
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
                SendCodeVerifications = registerUserSetting.SendCodeVerifications
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