using Microsoft.AspNetCore.Http;
using System;

namespace DataTransfer.UserDtos
{
    public class EditUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
