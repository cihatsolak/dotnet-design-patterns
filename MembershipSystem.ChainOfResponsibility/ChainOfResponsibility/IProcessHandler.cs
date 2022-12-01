namespace MembershipSystem.ChainOfResponsibility.ChainOfResponsibility
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler processHandler);
        object Handle(object obj);
    }
}
