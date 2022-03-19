namespace SmartCharging.Domain.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CapacityInAmps  { get; set; }
    public IList<ChargeStation> ChargeStations { get; set; }
}