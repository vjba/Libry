namespace Libry.Domain.Result;

public class Result<TDto> where TDto : class
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public TDto Data { get; }

    private Result(bool isSuccess, Error error, TDto data)
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

    public static Result<TDto> Success(TDto data) => new(true, Error.None, data);

    public static Result<TDto> Failure(Error error) => new(false, error, null!);
}
