using System;

namespace Catalog.Order.Domain.Common.Abstractions;

public record Result<T>(T? Value, bool IsSuccess, string? Error)
{
    public bool IsFailure => !IsSuccess; 

    public static Result<T> Success(T value) => new(value, true, null);
    public static Result<T> Failure(string error) => new(default, false, error);
}
