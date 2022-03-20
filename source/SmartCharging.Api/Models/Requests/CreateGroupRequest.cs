namespace SmartCharging.Api.Models.Requests;

public class CreateGroupRequest
{
    public string Name { get; set; }
    public int CapacityInAmps  { get; set; }
}