using System;
using PM.Bazaar.Application.Interfaces.Common;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;

namespace PM.Bazaar.Application.ApplicationServices.Common
{
    public class ApplicationService : IApplicationService, IDisposable
    {
        private readonly IUoW _uow;
        private bool _disposed;

        public ApplicationService(IUoW uow)
        {
            _uow = uow;
        }

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
