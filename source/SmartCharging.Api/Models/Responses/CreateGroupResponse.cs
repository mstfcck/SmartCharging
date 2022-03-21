namespace SmartCharging.Api.Models.Responses;

public class CreateGroupResponse
{
    public CreateGroupResponse(int id)
    {
        Id = id;
    }
    
    public int Id { get; private set; }
}