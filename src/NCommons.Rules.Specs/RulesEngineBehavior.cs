using Machine.Specifications;

namespace NCommons.Rules.Specs
{
    [Behaviors]
    public class RulesEngineBehavior
    {
        protected static ProcessResult _processResults;

        It should_return_process_results = () => _processResults.ShouldNotBeNull();
    }
}