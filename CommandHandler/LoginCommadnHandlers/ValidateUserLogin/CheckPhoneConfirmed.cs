namespace CommandHandler.LoginCommadnHandlers.ValidateUserLogin
{
    public class CheckPhoneConfirmed : ValidateUser
    {
        public CheckPhoneConfirmed(ValidateUser validateUser) : base(validateUser)
        {
        }

        public override UserContext ValidateUserLogin(UserContext request)
        {
            if (request.Context.ConfirmPhoneNumber)
            {
                return _validateUser.ValidateUserLogin(request);
            }
            return new UserContext
            {
                Message="Phone Number Not confirmed"
            };
        }
    }
}
