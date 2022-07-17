namespace Domain.Seedwork;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync();
}
