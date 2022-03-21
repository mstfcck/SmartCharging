using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationCommand : IRequest<CreateChargeStationDTO>
{
    public CreateChargeStationCommand(int byGroupId)
    {
        ByGroupId = byGroupId;
    }
    
    // Where

    public int ByGroupId { get; private set; }
    
    // Create
    
    public string Name { get; set; }
}

public class CreateChargeStationDTO
{
    public int Id { get; set; }
}