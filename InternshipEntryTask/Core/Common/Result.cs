namespace InternshipEntryTask.Core.Common;

public class Result
{
    
}

public class Result<TError> : Result where TError : Enum
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    
    public TError? Error { get; }

    protected Result(bool isSuccess, TError? error = default, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorMessage = errorMessage;
    }

    public static Result<TError> Success() => new (true);
    public static Result<TError> Failure(TError? error = default, string? errorMessage = null) => new (false, error, errorMessage);
}

public class Result<T, TError> : Result<TError> where TError : Enum
{
    public T? Value { get; }

    protected Result(bool isSuccess, T? value, TError? error = default, string? errorMessage = null)
        : base(isSuccess, error, errorMessage)
    {
        Value = value;
    }

    public static Result<T, TError> Success(T value) =>
        new (true, value, default, null);

    public new static Result<T, TError> Failure(TError error, string? errorMessage = null) =>
        new (false, default, error, errorMessage);
}