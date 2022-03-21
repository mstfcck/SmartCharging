namespace SmartCharging.Api.Models.Responses;

public class CreateConnectorResponse
{
    public CreateConnectorResponse(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}