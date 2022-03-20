using MediatR;
using SmartCharging.Application.Group.Commands.DeleteGroup;

namespace SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;

public class DeleteChargeStationHandler : IRequestHandler<DeleteGroupCommand>
{
    public DeleteChargeStationHandler()
    {
    }
    
    public Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}