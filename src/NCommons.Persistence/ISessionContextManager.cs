namespace NCommons.Persistence
{
    public interface IActiveSessionManager<TSession>
    {
        bool HasActiveSession { get; }
        TSession GetActiveSession();
        void SetActiveSession(TSession session);
        void ClearActiveSession();
    }
}