using MediatR;

namespace SmartCharging.Application.ChargeStation.Queries.GetChargeStationsQuery;

public class GetChargeStationsQuery : IRequest<GetChargeStationsDTO>
{
    public GetChargeStationsQuery(int groupId)
    {
        GroupId = groupId;
    }
    
    public int GroupId { get; private set; }
}

public class GetChargeStationsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
}