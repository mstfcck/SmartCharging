namespace SmartCharging.Api.Models.Requests;

public class UpdateGroupRequest
{
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
}