using Common.Operation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;

namespace Command.UserCommands
{
    public class CreateUserCommand : IRequest<OperationResult<string>>
    {
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public IFormFile Photo { get; private set; }
        public CreateUserCommand(string userName, string password, string name, string family, string phoneNumber, string email, IFormFile photo)
        {
            Username = userName;
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Password = password;
            Email = email;
            Photo = photo;
        }
    }
}
