namespace SmartCharging.Domain.Entities;

public class ChargeStation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Group Group { get; set; }
    public int GroupId { get; set; }
    public IList<Connector> Connectors { get; set; }
}