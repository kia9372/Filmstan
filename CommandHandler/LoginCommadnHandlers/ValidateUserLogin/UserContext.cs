using Domain.Aggregate.DomainAggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandHandler.LoginCommadnHandlers.ValidateUserLogin
{
    public class UserContext
    {
        public string Message { get; set; }
        public User Context { get; set; }
    }
}
