using Armyknife.Exceptions;
using Armyknife.Services.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Armyknife.Services.Implementations
{
    internal class ProcessService : IProcessService
    {
        public void StartProcess(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {path}")
                {
                    CreateNoWindow = true
                });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", path);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", path);
            }
            else
            {
                throw new ArmyknifeException("Direct opening files on OS not yet supported.");
            }
        }
    }
}
