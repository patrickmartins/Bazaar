namespace PM.Bazaar.Application.Interfaces.Common
{
    public interface IApplicationService
    {
        void BeginTransaction();
        void Commit();
    }
}
