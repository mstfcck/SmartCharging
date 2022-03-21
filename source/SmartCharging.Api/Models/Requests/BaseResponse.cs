namespace SmartCharging.Api.Models.Requests;

public class Response<TObject>
{
    public Response(TObject result)
    {
        Result = result;
    }

    public TObject Result { get; }
}