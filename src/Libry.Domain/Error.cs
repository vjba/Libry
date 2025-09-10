using System.Net;

namespace Libry.Domain;

public sealed record Error(HttpStatusCode HttpStatusCode, string Description)
{
    public static readonly Error None = new(HttpStatusCode.NoContent, string.Empty);
}
