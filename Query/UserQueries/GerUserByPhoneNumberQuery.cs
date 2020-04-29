using Common.Operation;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using MediatR;

namespace Query.UserQueries
{
    public class GerUserByPhoneNumberQuery : IRequest<OperationResult<User>>
    {
        public string PhoneNumber { get; private set; }
        public GerUserByPhoneNumberQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
