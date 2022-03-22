using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.Connector.Commands.DeleteConnector;

public class DeleteConnectorCommandHandler : IRequestHandler<DeleteConnectorCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public DeleteConnectorCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteConnectorCommand request, CancellationToken cancellationToken)
    {
        var connector = await _unitOfWork.Repository<Domain.Entities.Connector>().Read()
            .Where(x => x.Id == request.ConnectorId && 
                        x.ChargeStationId == request.ChargeStationId &&
                        x.ChargeStation.GroupId == request.GroupId)
            .Include(x => x.ChargeStation)
            .FirstOrDefaultAsync(cancellationToken);

        if (connector == null)
        {
            throw new BusinessException(ExceptionMessages.ConnectorCouldNotBeFound);
        }
        
        // PS: The transaction isn't worked while using a memory database.

        await _unitOfWork.BeginTransactionAsync();

        _unitOfWork.Repository<Domain.Entities.Connector>().Delete(connector);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();
        
        return Unit.Value;
    }
}