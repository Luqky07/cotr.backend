using cotr.backend.Model;

namespace cotr.backend.Service.Command
{
    public interface ICommandService
    {
        CommandRun ExecuteCommand(string command, string arguments, string directory);
    }
}
