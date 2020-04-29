using Autofac;
using BehaviorHandler.PipeLineBehaviors.CategoryBehavior;
using BehaviorHandler.PipeLineBehaviors.LoggingBehaviors;
using BehaviorHandler.PipeLineBehaviors.PostMagazineBehaviors;
using BehaviorHandler.PipeLineBehaviors.RegisterUserBehavior;
using BehaviorHandler.PipeLineBehaviors.SettingSiteBehavior;
using BehaviorHandler.PipeLineBehaviors.ValidatorsBehaviors;
using Command.CategoryCommands;
using Command.PostMagazinrCommands;
using Command.SettingCommand;
using Command.UserCommands;
using Common.LifeTime;
using Common.Operation;
using MediatR;
using SiteService;

namespace Framework.Configuration
{
    public static class AutofacConfiguration
    {
        public static void AutoInjectServices(this ContainerBuilder container)
        {

            var assService = typeof(SiteServiceMarker).Assembly;

            container.RegisterAssemblyTypes(assService)
                .AssignableTo<IScoped>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public static void PipeLineBehaviorRegister(this ContainerBuilder container)
        {
            container.RegisterGeneric(typeof(LoggingBehavior<,>)).
                   As(typeof(IPipelineBehavior<,>));

            container.RegisterGeneric(typeof(ValidatorBehavior<,>)).
                   As(typeof(IPipelineBehavior<,>));

            container.RegisterType<CheckUserNameExistValidation<CreateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<CreateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckEmailExistValidation<CreateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<CreateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckPhoneNumbrExistValidation<CreateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<CreateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckUpdateUserNameExistValidation<UpdateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<UpdateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckUpdateEmailExistValidation<UpdateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<UpdateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckUpdatePhoneNumbrExistValidation<UpdateUserCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<UpdateUserCommand, OperationResult<string>>>();

            container.RegisterType<CheckRoleRegisterByUserValidation<SetRegisterUserSettingCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<SetRegisterUserSettingCommand, OperationResult<string>>>();

            container.RegisterType<CheckRoleRegisterByAdminValidation<SetRegisterUserSettingCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<SetRegisterUserSettingCommand, OperationResult<string>>>();

            container.RegisterType<CheckCategoryIdIsValidate<CreatePostMagazineCommands, OperationResult<string>>>()
                  .As<IPipelineBehavior<CreatePostMagazineCommands, OperationResult<string>>>();

            container.RegisterType<CheckCategoryIdIsExist<CreateCategoryCommand, OperationResult<string>>>()
                  .As<IPipelineBehavior<CreateCategoryCommand, OperationResult<string>>>();
        }
    }
}
