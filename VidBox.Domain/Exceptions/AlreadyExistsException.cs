using System.Net;

namespace VidBox.Domain.Exceptions;

public class AlreadyExistsException : ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;
    public override string TitleMessage { get; protected set; } = string.Empty;
}
