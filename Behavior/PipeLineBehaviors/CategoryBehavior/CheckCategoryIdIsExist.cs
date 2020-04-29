using Command.CategoryCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.CategoryBehavior
{
    public class CheckCategoryIdIsExist<TRequest, TResponse> : IPipelineBehavior<CreateCategoryCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckCategoryIdIsExist(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            if (request.ParentId != null)
            {
                var findUserName = await unitOfWork.CategoryRepository.GetCategoryByIdAsync((Guid)request.ParentId, cancellationToken);
                if (findUserName.Result != null)
                {
                    return OperationResult<string>.BuildFailure("Category NotFound");
                }
            }
            return await next();
        }
    }
}
