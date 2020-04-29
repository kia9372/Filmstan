using Common.Operation;
using MediatR;
using System;

namespace Command.UserCommands
{
    public class UpdateUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Photo { get; private set; }
        public UpdateUserCommand(Guid id, string userName, string name, string family, string phoneNumber, string email, string photo)
        {
            Id = id;
            Username = userName;
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Photo = photo;
        }
    }
}
