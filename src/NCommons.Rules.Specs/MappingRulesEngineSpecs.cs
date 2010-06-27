using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NCommons.Rules.Mapping;
using NCommons.Rules.Specs.TestModels;
using It = Machine.Specifications.It;

namespace NCommons.Rules.Specs
{
    [Subject(typeof (MappingRulesEngine))]
    public class when_processing_a_valid_message : given_a_mapping_rules_engine_context
    {
        static TestMessage _destinationMessage;
        static TestMessage _message;
        static Mock<AssociationConfiguration<TestMessage>> _mockAssociationConfiguration;
        static Mock<ICommand<TestMessage>> _mockCommand;
        protected static ProcessResult _processResults;

        Establish context = () =>
            {
                // message
                _message = new TestMessage();

                // destination message
                _destinationMessage = new TestMessage();

                // mapper

                MockMessageMapper.Setup(m => m.Map(_message, typeof (TestMessage), typeof (TestMessage)))
                    .Returns(() => _destinationMessage);

                // assocation configuration
                _mockAssociationConfiguration = new Mock<AssociationConfiguration<TestMessage>>();
                _mockAssociationConfiguration.Setup(c => c.GetDestinationMessageType()).Returns(typeof (TestMessage));

                // association locator
                MockServiceLocator
                    .Setup(locator => locator.GetInstance(typeof (AssociationConfiguration<TestMessage>)))
                    .Returns(_mockAssociationConfiguration.Object);

                // command
                _mockCommand = new Mock<ICommand<TestMessage>>();
                _mockCommand.Setup(c => c.Execute(_message)).Returns(new ReturnValue().SetValue("test"));

                MockMessageValidator.Setup(s => s.Validate(Moq.It.IsAny<object>()))
                    .Returns(new RuleValidationResult());

                // command locator
                MockServiceLocator
                    .Setup(locator => locator.GetAllInstances<ICommand<TestMessage>>())
                    .Returns(new[] {_mockCommand.Object});
            };

        Because of = () => _processResults = RulesEngine.Process(_message);

        Behaves_like<RulesEngineBehavior> rules_engine;

        It should_validate_the_mapped_object = () => MockMessageValidator.Verify(v => v.Validate(_destinationMessage));

        It should_be_successful = () => _processResults.Successful.ShouldBeTrue();

        It should_execute_the_command = () => _mockCommand.Verify(c => c.Execute(_destinationMessage));
    }

    public abstract class given_a_mapping_rules_engine_context
    {
        protected static Mock<IMessageMapper> MockMessageMapper;
        protected static Mock<IRulesValidator> MockMessageValidator;
        protected static Mock<IMissingCommandStrategy> MockMissingCommandStrategy;
        protected static Mock<IServiceLocator> MockServiceLocator;
        protected static RulesEngine RulesEngine;

        Establish context = () =>
            {
                MockServiceLocator = new Mock<IServiceLocator>();
                ServiceLocator.SetLocatorProvider(() => MockServiceLocator.Object);
                MockMessageValidator = new Mock<IRulesValidator>();
                MockMessageMapper = new Mock<IMessageMapper>();
                MockMissingCommandStrategy = new Mock<IMissingCommandStrategy>();
                RulesEngine = new MappingRulesEngine(MockMessageValidator.Object, MockMessageMapper.Object,
                                                     MockMissingCommandStrategy.Object);
            };
    }
}