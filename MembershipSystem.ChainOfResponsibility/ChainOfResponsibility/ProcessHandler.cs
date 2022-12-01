namespace MembershipSystem.ChainOfResponsibility.ChainOfResponsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler _nextProcessHandler;

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            return _nextProcessHandler = processHandler;
        }

        public virtual object Handle(object obj)
        {
            if (_nextProcessHandler is not null)
            {
                return _nextProcessHandler.Handle(obj);
            }

            return default;
        }
    }
}
