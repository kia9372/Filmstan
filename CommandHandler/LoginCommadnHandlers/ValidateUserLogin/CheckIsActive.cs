namespace CommandHandler.LoginCommadnHandlers.ValidateUserLogin
{
    public class CheckIsActive : ValidateUser
    {
        public CheckIsActive(ValidateUser validateUser) : base(validateUser)
        {
        }

        public override UserContext ValidateUserLogin(UserContext request)
        {
            if (request.Context.IsActive)
            {
                return _validateUser.ValidateUserLogin(request);
            }
            return new UserContext
            {
                Message = "User Not Active"
            };
        }
    }
}
