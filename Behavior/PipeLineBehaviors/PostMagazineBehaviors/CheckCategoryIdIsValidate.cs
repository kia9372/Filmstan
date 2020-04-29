using Command.PostMagazinrCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorHandler.PipeLineBehaviors.PostMagazineBehaviors
{
    public class CheckCategoryIdIsValidate<TRequest, TResponse> : IPipelineBehavior<CreatePostMagazineCommands, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CheckCategoryIdIsValidate(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreatePostMagazineCommands request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<string>> next)
        {
            var findUserName = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (findUserName.Result != null)
            {
                return OperationResult<string>.BuildFailure("Category NotFound");
            }
            return await next();
        }
    }
}
