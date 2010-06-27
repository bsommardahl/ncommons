using Machine.Specifications;
using System.Linq;

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

        Because of;

        It should_be_retrievable_by_type =
            () => _result.ReturnItems.Where(i => i.Type.Equals(typeof (string))).ShouldNotBeEmpty();
    }
}