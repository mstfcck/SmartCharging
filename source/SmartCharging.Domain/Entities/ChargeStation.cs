using SmartCharging.Domain.Repositories;

namespace SmartCharging.Domain.Entities;

public partial class ChargeStation : IEntityFramework, IHasConcurrencyToken
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int GroupId { get; private set; }
    public Group Group { get; private set; }
    public IList<Connector> Connectors { get; private set; }
    public long RowVersion { get; set; }
}

public partial class ChargeStation
{
    public ChargeStation()
    {
    }

    public ChargeStation(string name, int groupId)
    {
        GroupId = groupId;
        Name = name;
    }
    
    public void Update(string name)
    {
        Name = name;
    }
}