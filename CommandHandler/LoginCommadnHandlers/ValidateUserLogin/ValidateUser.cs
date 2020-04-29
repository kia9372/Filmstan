using Domain.Aggregate.DomainAggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandHandler.LoginCommadnHandlers.ValidateUserLogin
{
    public abstract class ValidateUser
    {
        protected readonly ValidateUser _validateUser;
        public ValidateUser(ValidateUser validateUser)
        {
            _validateUser = validateUser;
        }
        public abstract UserContext ValidateUserLogin(UserContext request);
    }
}
