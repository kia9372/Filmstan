using Command.BrandCommands;
using Common.Operation;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Org.BouncyCastle.Ocsp;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.BrandsCommandHandlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CreateBrandCommandHandler(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand(request.BrandName, request.ISOBrandName, request.CategoryId);
            var add = await unitOfWork.CategoryRepository.BrandRepository.AddBrandAsync(brand, cancellationToken);
            if (add.Success)
            {
                await unitOfWork.CommitSaveChangeAsync();
                return OperationResult<string>.BuildSuccessResult(add.Result);
            }
            return OperationResult<string>.BuildFailure(add.ErrorMessage);
        }
    }


}
