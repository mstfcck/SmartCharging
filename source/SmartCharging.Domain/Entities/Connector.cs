using SmartCharging.Domain.Repositories;

namespace SmartCharging.Domain.Entities;

public partial class Connector : IEntityFramework
{
    public int Id { get; set; }
    public int MaxCurrentInAmps  { get; set; }
    public int ChargeStationId { get; set; }
    public ChargeStation ChargeStation { get; set; }
}

public partial class Connector
{
    public Connector()
    {
    }

    public Connector(int maxCurrentInAmps, int chargeStationId)
    {
        MaxCurrentInAmps = maxCurrentInAmps;
        ChargeStationId = chargeStationId;
    }

    public void Update(int maxCurrentInAmps)
    {
        MaxCurrentInAmps = maxCurrentInAmps;
    }
}