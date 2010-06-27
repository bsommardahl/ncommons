namespace NCommons.Persistence
{
    /// <summary>
    /// Represents a specific database source.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Starts a session.
        /// </summary>
        /// <returns>a session instance</returns>
        IDatabaseSession OpenSession();

        /// <summary>
        /// Commits a session.
        /// </summary>
        void CommitSession();

        /// <summary>
        /// Retrieves the current session.
        /// </summary>
        /// <returns>the current session</returns>
        IDatabaseSession GetCurrentSession();
    }
}