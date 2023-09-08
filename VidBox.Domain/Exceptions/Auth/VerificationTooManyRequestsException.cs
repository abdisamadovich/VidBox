namespace VidBox.Domain.Exceptions.Auth
{
    public class VerificationTooManyRequestsException : BadRequestException
    {
        public VerificationTooManyRequestsException()
        {
            TitleMessage = "You tried more than limits!";
        }
    }
}
