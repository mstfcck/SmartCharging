namespace SmartCharging.Api.Models.Responses;

public class CreateChargeStationResponse
{
    public CreateChargeStationResponse(int id)
    {
        Id = id;
    }
    
    public int Id { get; private set; }
}