using System;

namespace Armyknife.Exceptions
{
    public class ArmyknifeException : Exception
    {
        public ArmyknifeException(string message) : base(message)
        {
        }
    }
}
