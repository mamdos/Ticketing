namespace Services.Common.Models;

public class ServiceResponse
{
    public bool IsSucceed { get; init; }
    public string? Message { get; init; }

    protected ServiceResponse() { }

    public static ServiceResponse Successful()
    {
        return new ServiceResponse { IsSucceed = true };
    }

    public static ServiceResponse Fail(string message)
    {
        return new ServiceResponse()
        {
            IsSucceed = false,
            Message = message
        };
    }
}

public class ServiceResponse<TData> : ServiceResponse
{
    public TData? Data { get; init; }

    public static ServiceResponse<TData> Successful(TData data)
    {
        return new ServiceResponse<TData>()
        {
            Data = data,
            IsSucceed = true
        };
    }
}
