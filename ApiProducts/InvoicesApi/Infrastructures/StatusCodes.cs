using OneOf;

namespace InvoicesApi.Infrastructures;

public enum StatusCodes
{
    Success,
    NotFound,
    BadRequest,
    Conflict,
    // Outros códigos de status
}

public class AppError
{
    public StatusCodes Code { get; }
    public string ErrorMessage { get; }
    public ResultType ResultType { get; }

    public AppError(StatusCodes code, string errorMessage, ResultType resultType)
    {
        Code = code;
        ErrorMessage = errorMessage;
        ResultType = resultType;
    }
}

public enum ResultType
{
    Validation,
    Application
}

public static class AppErrorExtensions
{
    public static bool IsSuccess<TResult>(this OneOf<TResult,AppError> oneOf)
    {
        return oneOf.IsT0;
    }

    public static bool IsError<TResult>(this OneOf<TResult, AppError> oneOf)
    {
        return oneOf.IsT1;
    }
}

public class ShouldNotFound : AppError
{
    public ShouldNotFound() : base(StatusCodes.NotFound, "Resource not found", ResultType.Validation) { }
}

public class ShouldConflict : AppError
{
    public ShouldConflict() : base(StatusCodes.Conflict, "Resource already exists", ResultType.Validation) { }
}