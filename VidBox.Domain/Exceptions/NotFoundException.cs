﻿using System.Net;

namespace VidBox.Domain.Exceptions
{
    public class NotFoundException : ClientException
    {
        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

        public override string TitleMessage { get; protected set; } = String.Empty;
    }
}
