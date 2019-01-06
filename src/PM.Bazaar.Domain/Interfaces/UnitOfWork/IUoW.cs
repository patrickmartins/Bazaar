using System;

namespace PM.Bazaar.Domain.Interfaces.UnitOfWork
{
    public interface IUoW : IDisposable
    {
        void BeginTransaction();
        void Commit();
    }
}
