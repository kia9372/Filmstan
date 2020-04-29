using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;

namespace Query.UserQueries
{
    public class GerUserByUsernameQuery : IRequest<OperationResult<User>>
    {
        public string Username { get; private set; }
        public GerUserByUsernameQuery(string userName)
        {
            Username = userName;
        }
    }
}
