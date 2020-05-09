using Common.Operation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Command.UserCommands
{
    public class UpdateUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Email { get; private set; }
        public IFormFile? Photo { get; private set; }
        public UpdateUserCommand(Guid id, string userName, string name, string family, string email, IFormFile? photo)
        {
            Id = id;
            Username = userName;
            Name = name;
            Family = family;
            Email = email;
            Photo = photo;
        }
    }
}
