using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneOf;

namespace DummyJsonApi.WebApi.Services
{
    public enum StatusCodes
    {
        Success,
        NotFound,
        BadRequest,
        Conflit,
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
        Application // Corrigido de `Aplication` para `Application`
    }

    public static class AppErrorExtensions
    {
        public static bool IsSuccess<TResult>(this OneOf<TResult, AppError> oneOf)
        {
            return oneOf.IsT0; // `IsT0` verifica se o `OneOf` contém o resultado de sucesso
        }

        public static bool IsError<TResult>(this OneOf<TResult, AppError> oneOf)
        {
            return oneOf.IsT1; // `IsT1` verifica se o `OneOf` contém um erro (AppError)
        }
    }

    public class ShouldNotFound() : AppError(StatusCodes.NotFound, "Resource not found", ResultType.Validation)
    {
    }

    public class ShouldConflit(): AppError(StatusCodes.Conflit, "Resource already there's", ResultType.Application);
}
