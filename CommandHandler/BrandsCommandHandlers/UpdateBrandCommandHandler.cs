using Command.BrandCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.BrandsCommandHandlers
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public UpdateBrandCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await unitOfWork.CategoryRepository.BrandRepository.GetBrandById(request.BrandId, cancellationToken);
            if (brand.Success)
            {
                brand.Result.SetValues(request.BrandName, request.ISOBrandName, request.CategoryId);
                var update = unitOfWork.CategoryRepository.BrandRepository.UpdateBrandAsync(brand.Result, cancellationToken);
                if (update.Success)
                {
                    await unitOfWork.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult(update.Result);
                }
                return OperationResult<string>.BuildFailure(update.ErrorMessage);
            }
            return OperationResult<string>.BuildFailure(brand.ErrorMessage);
        }
    }
}
