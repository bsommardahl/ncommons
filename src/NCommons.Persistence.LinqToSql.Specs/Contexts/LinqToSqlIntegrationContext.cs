using Machine.Specifications;

namespace NCommons.Persistence.LinqToSql.Specs.RepositorySpecs
{
    public abstract class given_a_linq_context
    {
        protected static LinqToSqlActiveSessionManager ActiveSessionManager;
        protected static LinqToSqlDatabaseContext DatabaseContext;

        Establish context = () =>
            {
                ActiveSessionManager = new LinqToSqlActiveSessionManager();
                DatabaseContext = new LinqToSqlDatabaseContext(ActiveSessionManager, () => new DataClassesDataContext());
            };
    }
}