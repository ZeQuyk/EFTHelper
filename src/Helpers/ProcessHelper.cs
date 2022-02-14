using System.Diagnostics;

namespace EFTHelper.Helpers
{
    public static class ProcessHelper
    {
        public static void StartProcess(string processName)
        {
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = processName
            };

            Process.Start(processStartInfo);
        }

    }
}
