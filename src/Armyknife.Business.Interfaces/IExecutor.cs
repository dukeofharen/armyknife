using System.Threading.Tasks;

namespace Armyknife.Business.Interfaces
{
    public interface IExecutor
    {
        Task<int> ExecuteAsync(string[] args);
    }
}
