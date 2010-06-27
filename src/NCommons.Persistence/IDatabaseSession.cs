using System;

namespace NCommons.Persistence
{
    public interface IDatabaseSession : IDisposable
    {
        void Flush();
        void Commit();
        void Rollback();
    }
}