using Command.CategoryPropertyCommands;
using Common.Operation;
using DataTransfer.CategoryPropertyDto;
using Domain.Aggregate.DomainAggregates.CategoryAggregate;
using MediatR;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SiteService.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler.CategoryPropertyCommandHandlers
{
    public class CreateCategoryPropertyCommandHandlers : IRequestHandler<CreateCategoryPropertyCommand, OperationResult<string>>
    {
        private readonly IDomainUnitOfWork unitOfWork;

        public CreateCategoryPropertyCommandHandlers(IDomainUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<string>> Handle(CreateCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var getAllPropByCategory = await unitOfWork.CategoryRepository.CategoryPropertyRepository.GetAllCategoryPropertybyCategoryId(request.CategoryPropertyDtos[0].CategoryId, cancellationToken);
            if (getAllPropByCategory.Success)
            {
                var currentValue = getAllPropByCategory.Result.Select(x => new CategoryPropertyDto
                {
                    CategoryId = x.CategoryId,
                    CategoryPropertyType = x.CategoryPropertyType.CategoryPropertyType,
                    Id = x.Id,
                    PropName = x.PropName
                }).ToList();
                /// Add New Property
                var newProp = request.CategoryPropertyDtos.Where(x => x.Id == null).ToList();

                if (newProp.Count() > 0)
                {
                    List<CategoryProperty> CategoryProperty = new List<CategoryProperty>();
                    foreach (var item in newProp)
                    {
                        CategoryProperty.Add(new CategoryProperty(item.PropName, item.CategoryPropertyType, item.CategoryId));
                    }
                    await unitOfWork.CategoryRepository.CategoryPropertyRepository.AddBulkCategoryProperty(CategoryProperty, cancellationToken);
                }
                /// Rempove Items
                var removeValue = currentValue.Select(x => x.Id).Except(request.CategoryPropertyDtos.Select(x => x.Id)).ToList();
                if (removeValue.Count() > 0)
                {
                    List<CategoryProperty> RemoveCategoryProperty = new List<CategoryProperty>();
                    foreach (var clientSend in removeValue)
                    {
                        var item = getAllPropByCategory.Result.FirstOrDefault(x => x.Id == clientSend);
                        if (item != null)
                        {
                            RemoveCategoryProperty.Add(item);
                        }
                    }
                    await unitOfWork.CategoryRepository.CategoryPropertyRepository.DeleteBulkCategoryProperty(RemoveCategoryProperty, cancellationToken);
                }

                ///Update Category Poeorpty
                foreach (var clientSend in request.CategoryPropertyDtos.Where(x => x.Id != null).ToList())
                {
                    var item = getAllPropByCategory.Result.FirstOrDefault(x => x.Id == clientSend.Id);
                    if (clientSend.PropName != item.PropName || clientSend.CategoryPropertyType != item.CategoryPropertyType.CategoryPropertyType)
                    {
                        if (item != null)
                        {
                            item.SetValues(clientSend.PropName, clientSend.CategoryPropertyType, clientSend.CategoryId);
                            unitOfWork.CategoryRepository.CategoryPropertyRepository.UpdateCategoryProperty(item, cancellationToken);
                        }
                    }
                }

                await unitOfWork.CommitSaveChangeAsync();
                return OperationResult<string>.BuildSuccessResult("Success Add");
            }
            return OperationResult<string>.BuildFailure(getAllPropByCategory.ErrorMessage);
        }
    }
}
