namespace DataAccess.Infrastructure
{
    public interface IBeforeCommitHandler
    {
        void Execute(ApplicationContext context);
    }
}
