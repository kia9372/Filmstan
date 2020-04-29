namespace CommandHandler.LoginCommadnHandlers.ValidateUserLogin
{
    public  class CheckIsLockedAccount : ValidateUser
    {
        public CheckIsLockedAccount(ValidateUser validateUser) : base(validateUser)
        {
        }

        public override UserContext ValidateUserLogin(UserContext request)
        {
            if (!request.Context.IsLockedEnd)
            {
                return new UserContext
                {
                    Context=request.Context
                };
            }
            return new UserContext
            {
                Message = $"Yuor Account Deactive From To Date {request.Context.LockedEnd}"
            };
        }
    }
}
