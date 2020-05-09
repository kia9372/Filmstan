using System;

namespace Command.UserCommands
{
    public class UpdatePhoneNumberCommand
    {
        public Guid UserId { get; set; }
        public string  PhoneNumber { get; set; }
    }
}
