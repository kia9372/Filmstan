using Common.Operation;
using MediatR;
using System;

public class ChangePasswordUserCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; private set; }
    public string Password { get; private set; }
    public ChangePasswordUserCommand(Guid id, string password)
    {
        Id = id;
        Password = password;
    }
}
