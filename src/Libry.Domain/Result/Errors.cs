using System.Net;

namespace Libry.Domain.Result;

public static class Errors
{
    public static readonly Error NotFound = new(HttpStatusCode.NotFound, "Entity not found");
    public static readonly Error InternalServerError = new(HttpStatusCode.InternalServerError, "A backend issue occured with your request");
}
