using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommandHandler : IRequestHandler<UpdateConnectorCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public UpdateConnectorCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
    {
        var connector = await _unitOfWork.Repository<Domain.Entities.Connector>().Read()
            .Where(x => x.Id == request.ByConnectorId && 
                        x.ChargeStationId == request.ByChargeStationId &&
                        x.ChargeStation.GroupId == request.ByGroupId)
            .Include(x => x.ChargeStation)
            .FirstOrDefaultAsync(cancellationToken);

        if (connector == null)
        {
            throw new BusinessException("Connector could not be found.");
        }
        
        connector.Update(request.MaxCurrentInAmps);
        
        // PS: The transaction isn't worked while using a memory database.

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Repository<Domain.Entities.Connector>().UpdateAsync(connector, request.ByConnectorId);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}