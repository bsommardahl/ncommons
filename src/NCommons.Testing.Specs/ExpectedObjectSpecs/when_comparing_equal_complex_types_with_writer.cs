using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using NCommons.Testing.Equality;
using It = Machine.Specifications.It;

namespace NCommons.Testing.Specs.ExpectedObjectSpecs
{
    public class when_comparing_equal_complex_types_with_writer
    {
        static readonly List<EqualityResult> _results = new List<EqualityResult>();
        static object _actual;
        static object _expected;
        static Mock<IWriter> _mockWriter;

        Establish context = () =>
            {
                _mockWriter = new Mock<IWriter>();
                _mockWriter.Setup(x => x.Write(Moq.It.IsAny<EqualityResult>()))
                    .Callback<EqualityResult>(result => _results.Add(result));

                _expected = new
                                {
                                    Level1 = new
                                                 {
                                                     Level2 = new
                                                                  {
                                                                      Level3 = "test"
                                                                  }
                                                 }
                                }.ToExpectedObject().Configure(ctx => ctx.WithWriter(_mockWriter.Object));

                _actual = new
                              {
                                  Level1 = new
                                               {
                                                   Level2 = new
                                                                {
                                                                    Level3 = "test2"
                                                                }
                                               }
                              };
            };

        Because of = () => _expected.Equals(_actual);

        It should_write_the_full_accumulated_path_for_the_unqual_member =
            () =>
            (_results.Where(r => r.Member.Contains("Level1.Level2.Level3")).Select(r => r.Member)).SingleOrDefault().
                ShouldNotBeNull();
    }
}