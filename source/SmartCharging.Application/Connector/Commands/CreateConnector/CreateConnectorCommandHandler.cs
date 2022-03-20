using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommandHandler : IRequestHandler<CreateConnectorCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public CreateConnectorCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateConnectorCommand request, CancellationToken cancellationToken)
    {
        var chargeStation = await _unitOfWork.Repository<Domain.Entities.ChargeStation>().Read()
            .Where(x => x.Id == request.ByChargeStationId && x.GroupId == request.ByGroupId)
            .Include(x => x.Connectors)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (chargeStation == null)
        {
            throw new BusinessException("Connector could not be found.");
        }

        if (chargeStation.Connectors.Count >= 5)
        {
            throw new BusinessException("You cannot add more than 5 connectors.");
        }
        
        var connector = new Domain.Entities.Connector(request.MaxCurrentInAmps, request.ByChargeStationId);
        
        // PS: The transaction isn't worked while using a InMemory database.

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Repository<Domain.Entities.Connector>().CreateAsync(connector);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}