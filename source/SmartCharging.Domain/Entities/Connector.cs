using SmartCharging.Domain.Repositories;

namespace SmartCharging.Domain.Entities;

public partial class Connector : IEntityFramework, IHasConcurrencyToken
{
    public int Id { get; private set; }
    public int MaxCurrentInAmps { get; private set; }
    public int ChargeStationId { get; private set; }
    public ChargeStation ChargeStation { get; private set; }
    public long RowVersion { get; set; }
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