using Command.BrandCommands;
using Common.Operation;
using MediatR;
using SiteService.Repositories.Implementation;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.BrandsCommandHandlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public DeleteBrandCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await unitOfWork.CategoryRepository.BrandRepository.GetBrandById(request.BrandId, cancellationToken);
            if (brand.Success)
            {
                brand.Result.Delete();
                var add = unitOfWork.CategoryRepository.BrandRepository.UpdateBrandAsync(brand.Result, cancellationToken);
                if (add.Success)
                {
                    await unitOfWork.CommitSaveChangeAsync();
                    return OperationResult<string>.BuildSuccessResult(add.Result);
                }
                return OperationResult<string>.BuildFailure(add.ErrorMessage);
            }
            return OperationResult<string>.BuildFailure(brand.ErrorMessage);
        }
    }
}
