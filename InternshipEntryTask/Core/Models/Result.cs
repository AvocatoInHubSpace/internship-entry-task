namespace InternshipEntryTask.Core.Models;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    public Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new (true, null);
    public static Result Failure(string? error = null) => new (false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    public Result(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) =>
        new (true, value, null);

    public new static Result<T> Failure(string? error = null) =>
        new (false, default, error);

}