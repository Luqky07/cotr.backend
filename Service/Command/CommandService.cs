using cotr.backend.Model;
using System.Diagnostics;

namespace cotr.backend.Service.Command
{
    public class CommandService : ICommandService
    {
        public CommandRun ExecuteCommand(string command, string arguments, string directory)
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = directory
            };

            using Process process = new() { StartInfo = processStartInfo };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            return new(output, error);
        }
    }
}
