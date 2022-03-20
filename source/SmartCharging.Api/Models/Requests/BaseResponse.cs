namespace SmartCharging.Api.Models.Requests;

public class BaseResponse<T>
{
    public T Result { get; set; }
}