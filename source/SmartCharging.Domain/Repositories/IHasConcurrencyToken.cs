namespace SmartCharging.Domain.Repositories;

public interface IHasConcurrencyToken
{
    public long RowVersion { get; set; }
}