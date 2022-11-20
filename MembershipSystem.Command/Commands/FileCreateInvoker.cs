namespace MembershipSystem.Command.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand _tableActionCommand;
        private readonly List<ITableActionCommand> TableActionCommands = new();

        public void SetCommand(ITableActionCommand tableActionCommand)
        {
            _tableActionCommand = tableActionCommand;
        }

        public void AddCommand(ITableActionCommand tableActionCommand)
        {
            TableActionCommands.Add(tableActionCommand);
        }

        public IActionResult CreateFile()
        {
            return _tableActionCommand.Execute();
        }

        public List<IActionResult> CreateFiles()
        {
            return TableActionCommands.Select(x => x.Execute()).ToList();
        }
    }
}
