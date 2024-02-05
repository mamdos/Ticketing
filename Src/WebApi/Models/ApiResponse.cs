namespace WebApi.Models;


public sealed class ApiResponse
{
    public object? Data { get; init; }
    public bool IsSucceed { get; init; }
    public string? Message { get; init; }

    private ApiResponse() { }

    public static ApiResponse Successful(object? data = null)
    {
        return new ApiResponse
        {
            Data = data,
            IsSucceed = true,
        };
    }

    public static ApiResponse Fail(object? data = null, string? message = null)
    {
        return new ApiResponse
        {
            Data = data,
            IsSucceed = false,
            Message = message
        };
    }
}