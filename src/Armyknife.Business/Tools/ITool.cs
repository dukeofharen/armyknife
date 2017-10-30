namespace Armyknife.Business.Tools
{
    public interface ITool
    {
        string Name { get; }

        string Description { get; }

        string Category { get; }

        string HelpText { get; }
    }
}
