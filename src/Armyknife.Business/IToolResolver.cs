using Armyknife.Business.Tools;

namespace Armyknife.Business
{
    public interface IToolResolver
    {
        ITool ResolveTool(string name);
    }
}
