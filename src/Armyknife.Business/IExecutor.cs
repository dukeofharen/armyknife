using System.Threading.Tasks;

namespace Armyknife.Business
{
    public interface IExecutor
    {
        Task ExecuteAsync(string[] args);
    }
}
