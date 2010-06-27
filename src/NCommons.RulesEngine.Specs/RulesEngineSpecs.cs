using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace NCommons.RulesEngine.Specs
{
    [Subject(typeof (RulesEngine))]
    public class Scenario
    {
        Establish context = () => { };

        Because of = () => { };

        It Observation = () => { };
    }
}
