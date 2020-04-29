
using System.Threading.Tasks;

namespace Domain.Core.UnitOfWork
{
    public interface IUnitOfWork 
    {
        void CommitSaveChange();
        Task CommitSaveChangeAsync();
    }
}
