using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Utilitis
{
    public class ConfirmPasswordAttribute : ValidationAttribute
    {
        private readonly string password;

        public ConfirmPasswordAttribute(string password)
        {
            this.password = password;
        }
        public override bool IsValid(object value)
        {
            if (password == value.ToString())
                return true;
            return false;
        }
    }
}
