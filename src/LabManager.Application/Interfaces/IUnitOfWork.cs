using System.Threading;
using System.Threading.Tasks;

namespace LabManager.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
