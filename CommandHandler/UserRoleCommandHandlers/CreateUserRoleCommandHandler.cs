using Command.UserRoleCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.UserRoleCommandHandlers
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, OperationResult<bool>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CreateUserRoleCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<bool>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var add = await unitOfWork.UsersRepository.UsersRoleRepository.AddAsync(new UserRole(request.RoleId, request.UserId), cancellationToken);
            if (add.Success)
            {
                return OperationResult<bool>.BuildSuccessResult(true);
            }
            return OperationResult<bool>.BuildFailure(add.ErrorMessage);
        }
    }

}
