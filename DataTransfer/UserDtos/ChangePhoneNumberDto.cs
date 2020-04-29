using System;

namespace DataTransfer.UserDtos
{
    public class ChangePhoneNumberDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ChangeUserActiveStatusDto
    {
        public Guid Id { get; set; }
    }

    public class ConfirmEmailDto
    {
        public string Email { get; set; }
    }
}
