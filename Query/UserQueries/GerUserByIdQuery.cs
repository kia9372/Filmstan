using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;
using System;
using System.Text;

namespace Query.UserQueries
{
    public class GerUserByIdQuery : IRequest<OperationResult<User>>
    {
        public Guid Id { get; private set; }
        public GerUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
