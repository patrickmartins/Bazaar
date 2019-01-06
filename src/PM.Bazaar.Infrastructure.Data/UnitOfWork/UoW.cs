using PM.Bazaar.Infrastructure.Data.Contexts;
using System;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;

namespace PM.Bazaar.Infrastructure.Data.UnitOfWork
{
    public class UoW : IUoW
    {
        private readonly BazaarContext _context;
        private bool _disposed;

        public UoW(BazaarContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
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
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
