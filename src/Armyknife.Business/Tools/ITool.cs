﻿using System.Collections.Generic;

namespace Armyknife.Business.Tools
{
    public interface ITool
    {
        string Name { get; }

        string Description { get; }

        string Category { get; }

        string HelpText { get; }

        byte[] Execute(IDictionary<string, string> args);
    }
}