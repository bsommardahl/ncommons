using System.Linq;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NCommons.Rules.Specs.TestModels;
using It = Machine.Specifications.It;

namespace NCommons.Rules.Specs
{
    [Subject(typeof (RulesEngine))]
    public class when_processing_a_valid_message_which_has_a_matching_command_with_results :
        given_a_standard_rules_engine_context
    {
        static TestMessage _message;
        static Mock<ICommand<TestMessage>> _mockCommand;
        protected static ProcessResult _processResults;
        static RuleValidationResult _ruleValidationResult;

        Establish additional_context = () =>
            {
                _message = new TestMessage();
                _mockCommand = new Mock<ICommand<TestMessage>>();
                _mockCommand.Setup(c => c.Execute(_message)).Returns(new ReturnValue().SetValue("test"));

                _ruleValidationResult = new RuleValidationResult();

                MockMessageValidator.Setup(s => s.Validate(Moq.It.IsAny<object>()))
                    .Returns(_ruleValidationResult);

                MockServiceLocator
                    .Setup(locator => locator.GetAllInstances<ICommand<TestMessage>>())
                    .Returns(new[] {_mockCommand.Object});
            };

        Because of = () => _processResults = RulesEngine.Process(_message);

        Behaves_like<RulesEngineBehavior> rules_engine;

        It should_be_successful = () => _processResults.Successful.ShouldBeTrue();

        It should_execute_the_command = () => _mockCommand.Verify(c => c.Execute(_message));

        It should_have_return_items = () => _processResults.ReturnItems.Count().ShouldEqual(1);
    }

    [Subject(typeof (RulesEngine))]
    public class when_processing_an_invalid_message_which_has_a_matching_command :
        given_a_standard_rules_engine_context
    {
        static TestMessage _message;
        static Mock<ICommand<TestMessage>> _mockCommand;
        protected static ProcessResult _processResults;
        static RuleValidationResult _ruleValidationResult;

        Establish additional_context = () =>
            {
                _message = new TestMessage();
                _mockCommand = new Mock<ICommand<TestMessage>>();

                _ruleValidationResult = new RuleValidationResult();
                _ruleValidationResult.AddValidationFailure(new RuleValidationFailure("An error occurred", string.Empty));

                MockMessageValidator.Setup(s => s.Validate(Moq.It.IsAny<object>()))
                    .Returns(_ruleValidationResult);

                MockServiceLocator
                    .Setup(locator => locator.GetAllInstances<ICommand<TestMessage>>())
                    .Returns(new[] {_mockCommand.Object});
            };

        Because of = () => _processResults = RulesEngine.Process(_message);

        Behaves_like<RulesEngineBehavior> rules_engine;

        It should_be_unsuccessful = () => _processResults.Successful.ShouldBeFalse();

        It should_not_execute_the_command = () => _mockCommand.Verify(c => c.Execute(_message), Times.Never());

        It should_return_results_with_at_least_one_message = () => _processResults.ValidationFailures.ShouldNotBeEmpty();
    }

    [Subject(typeof (RulesEngine))]
    public class when_processing_a_valid_message_without_a_matching_command : given_a_standard_rules_engine_context
    {
        static TestMessage _message;
        protected static ProcessResult _processResults;
        static RuleValidationResult _ruleValidationResult;

        Establish additional_context = () =>
            {
                _message = new TestMessage();

                _ruleValidationResult = new RuleValidationResult();

                MockMessageValidator.Setup(s => s.Validate(Moq.It.IsAny<object>()))
                    .Returns(_ruleValidationResult);
            };

        Because of = () => _processResults = RulesEngine.Process(_message);

        Behaves_like<RulesEngineBehavior> rules_engine;

        It should_execute_missing_command_strategy =
            () => MockMissingCommandStrategy.Verify(s => s.Execute(_message), Times.Exactly(1));
    }

    [Tags("integration")]
    [Subject(typeof (RulesEngine))]
    public class when_processing_a_message_without_a_matching_command_with_default_missing_command_strategy
    {
        static TestMessage _message;
        protected static Mock<IRuleValidator> _mockMessageValidator;
        protected static Mock<IServiceLocator> _mockServiceLocator;
        protected static ProcessResult _processResults;
        static RuleValidationResult _ruleValidationResult;
        protected static RulesEngine _rulesEngine;

        Establish context = () =>
            {
                _mockServiceLocator = new Mock<IServiceLocator>();
                ServiceLocator.SetLocatorProvider(() => _mockServiceLocator.Object);
                _mockMessageValidator = new Mock<IRuleValidator>();
                _message = new TestMessage();
                _ruleValidationResult = new RuleValidationResult();
                _mockMessageValidator.Setup(s => s.Validate(Moq.It.IsAny<object>()))
                    .Returns(_ruleValidationResult);
                _rulesEngine = new RulesEngine(_mockMessageValidator.Object, new MissingCommandStrategy());
            };

        Because of = () => _processResults = _rulesEngine.Process(_message);

        Behaves_like<RulesEngineBehavior> rules_engine;

        It should_be_successful = () => _processResults.Successful.ShouldBeTrue();
    }

    public abstract class given_a_standard_rules_engine_context
    {
        protected static Mock<IRuleValidator> MockMessageValidator;
        protected static Mock<IMissingCommandStrategy> MockMissingCommandStrategy;
        protected static Mock<IServiceLocator> MockServiceLocator;
        protected static RulesEngine RulesEngine;

        Establish context = () =>
            {
                MockServiceLocator = new Mock<IServiceLocator>();
                ServiceLocator.SetLocatorProvider(() => MockServiceLocator.Object);
                MockMessageValidator = new Mock<IRuleValidator>();
                MockMissingCommandStrategy = new Mock<IMissingCommandStrategy>();
                RulesEngine = new RulesEngine(MockMessageValidator.Object, MockMissingCommandStrategy.Object);
            };
    }
}