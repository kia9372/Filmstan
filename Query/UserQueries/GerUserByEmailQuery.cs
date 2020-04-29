using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;

namespace Query.UserQueries
{
    public class GerUserByEmailQuery : IRequest<OperationResult<User>>
    {
        public string Email { get; private set; }
        public GerUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
