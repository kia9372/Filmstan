using Command.UserCommands;
using Command.UserRoleCommands;
using CommandHandler.UserRoleCommandHandlers;
using Common.FilmStanEnums;
using Common.FilmstanExtentions;
using Common.Notification;
using Common.Operation;
using Common.SiteEnums;
using Common.UploadUtility;
using Common.Utilitis;
using DataTransfer.EmailSettingDtos;
using DataTransfer.Setting;
using DataTransfer.SMSSettingDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Domain.Aggregate.DomainAggregates.UserAggregate.ValueObjects;
using MediatR;
using MimeKit;
using SiteService.Repositories.Implementation;
using SiteService.SendNotificationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserCommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;
        private readonly IEnumerable<SendNotif> sendNotifs;
        private readonly IMediator mediator;

        public CreateUserCommandHandler(IDomainUnitOfWork unitOfWork, IEnumerable<SendNotif> sendNotifs, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.sendNotifs = sendNotifs;
            this.mediator = mediator;
        }
        public async Task<OperationResult<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string fileName = null;
            if (request.Photo != null)
            {
                var uploadFile = await UploadUtiltie.UploadInCustomePath(request.Photo, ".png", request.Username, UploadFolderPath.PathUserUploadFolder(), UploadFolderPath.PathAvatarUserUploadFolder());
                fileName = uploadFile.Result;
            }
            /// Register User
            var user = new User(request.Username, request.Password, request.Name, request.Family, request.PhoneNumber, request.Email, fileName);
            var addUser = await unitOfWork.UsersRepository.AddAsync(user, cancellationToken);
            if (addUser.Success)
            {
                try
                {
                    var registerSetting = await unitOfWork.SettingRepository.Get<RegisterUserSetting>(SettingEnum.RegisterUserSetting.EnumToString(), cancellationToken);
                    if (registerSetting.Result != null)
                    {
                        /// Add User  Role
                        var addUserRole = await mediator.Send(new CreateUserRoleCommand(registerSetting.Result.RegisterRoleByAdmin, user.Id));
                        if (addUserRole.Success)
                        {
                            /// Add User Generation Code
                            ActivationCode acCode = new ActivationCode(user.Id, CodeTypes.PhoneConfirmed, Utility.Hash(user.Username));
                            var gerateActivationCode = await unitOfWork.UsersRepository.ActivationCodeRepository.AddAsync(acCode, cancellationToken);
                            if (gerateActivationCode.Success)
                            {
                                /// Send Registration Code
                                SendNotif sendNotif = sendNotifs.Where(x => x.SendCodeType == registerSetting.Result.SendCodeVerifications)
                                    .FirstOrDefault();
                                var sendCode = await sendNotif.SendCodeAsync(gerateActivationCode.Result.Item2.ToString(), user.PhoneNumber, cancellationToken);
                                if (sendCode.Success)
                                {
                                    /// Save to Database
                                    await unitOfWork.CommitSaveChangeAsync();
                                    return OperationResult<string>.BuildSuccessResult(gerateActivationCode.Result.Item1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return OperationResult<string>.BuildFailure(ex.Message);
                }
            }
            return OperationResult<string>.BuildFailure(addUser.ErrorMessage);
        }
    }
}
