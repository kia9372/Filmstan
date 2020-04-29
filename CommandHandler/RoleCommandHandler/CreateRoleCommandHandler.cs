using Command.RoleCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.RoleAggregate;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.RoleCommandHandler
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CreateRoleCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var add = await unitOfWork.RoleRepository.AddAsync(new Role(request.Name, request.Description), cancellationToken);
            if (add.Success)
            {
                try
                {
                    await unitOfWork.CommitSaveChangeAsync();
                }
                catch (Exception ex)
                {
                    return OperationResult<string>.BuildFailure(ex);
                }
                return OperationResult<string>.BuildSuccessResult("Success Add");
            }
            return OperationResult<string>.BuildFailure(add.ErrorMessage);
        }
    }
}
