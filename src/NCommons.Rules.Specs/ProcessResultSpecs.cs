using System.Linq;
using Machine.Specifications;

namespace NCommons.Rules.Specs
{
    [Subject(typeof (ProcessResult))]
    public class when_return_items_are_present
    {
        static ProcessResult _result;

        Establish context = () =>
            {
                _result = new ProcessResult();
                _result.AddReturnItem(new ReturnValue().SetValue("string"));
            };

        It should_be_retrievable_by_type =
            () => _result.ReturnItems.Where(i => i.Type.Equals(typeof (string))).ShouldNotBeEmpty();
    }

    public class when_adding_a_validation_failure
    {
        static ProcessResult _result;
        static ProcessResult _value;

        Establish context = () => { _result = new ProcessResult(); };

        Because of = () => _value = _result.AddValidationFailure(new RuleValidationFailure("test", "test"));

        It should_return_the_result = () => _value.ShouldEqual(_result);
    }

    public class when_adding_a_return_item
    {
        static ReturnValue _returnValue;
        static ProcessResult _result;
        static ProcessResult _value;

        Establish context = () =>
            {
                _result = new ProcessResult();
                _returnValue = new ReturnValue();
            };

        Because of = () => _value = _result.AddReturnItem(new ReturnValue());

        It should_return_the_result = () => _value.ShouldEqual(_result);

        It should_return_a_result_containing_the_return_item =
            () => _value.ReturnItems.Where(x => x.Value.Equals(_returnValue));
    }
}