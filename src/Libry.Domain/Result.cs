namespace Libry.Domain;

public class Result<TData> where TData : class
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public TData Data { get; }

    private Result(bool isSuccess, Error error, TData data)
    {
        if (isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }

    public static Result<TData> Success(TData data) => new(true, Error.None, data);

    public static Result<TData> Failure(Error error) => new(false, error, null!);
}
