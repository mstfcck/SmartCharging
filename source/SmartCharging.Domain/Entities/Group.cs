using SmartCharging.Domain.Repositories;

namespace SmartCharging.Domain.Entities;

public partial class Group : IEntityFramework
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int CapacityInAmps  { get; private set; }
    public IList<ChargeStation> ChargeStations { get; set; }
}

public partial class Group
{
    public Group()
    {
    }
    
    public Group(string name, int capacityInAmps)
    {
        CapacityInAmps = capacityInAmps;
        Name = name;
    }

    public void Update(string name, int capacityInAmps)
    {
        Name = name;
        CapacityInAmps = capacityInAmps;
    }
}