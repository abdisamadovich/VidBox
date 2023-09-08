namespace VidBox.Domain.Exceptions.Auth
{
    public class VerificationCodeExpiredException : BadRequestException
    {
        public VerificationCodeExpiredException()
        {
            TitleMessage = "Verification code is expired!";
        }
    }
}
