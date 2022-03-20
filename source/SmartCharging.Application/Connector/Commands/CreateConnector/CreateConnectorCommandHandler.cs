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

        var group = await _unitOfWork.Repository<Domain.Entities.Group>().Read()
            .Include(x => x.ChargeStations)
            .ThenInclude(x => x.Connectors)
            .FirstOrDefaultAsync(x => x.Id == request.ByGroupId, cancellationToken);

        if (group == null)
        {
            throw new BusinessException("Group could not be found.");
        }

        if (group.CapacityInAmps < group.ChargeStations.Sum(x => x.Connectors.Sum(y => y.MaxCurrentInAmps) + request.MaxCurrentInAmps))
        {
            throw new BusinessException("The capacity in Amps of a Group is not enough.");
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