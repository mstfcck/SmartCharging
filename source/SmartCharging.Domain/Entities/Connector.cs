namespace SmartCharging.Domain.Entities;

public class Connector
{
    public int Id { get; set; }
    public int MaxCurrentInAmps  { get; set; }
    public ChargeStation ChargeStation { get; set; }
    public int ChargeStationId { get; set; }
}