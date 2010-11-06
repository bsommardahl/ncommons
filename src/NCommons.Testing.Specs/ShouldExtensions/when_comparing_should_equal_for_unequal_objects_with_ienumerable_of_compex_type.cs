using System;
using System.Collections.Generic;
using Machine.Specifications;
using NCommons.Testing.Equality;
using NCommons.Testing.Specs.TestTypes;

namespace NCommons.Testing.Specs.ShouldExtensions
{
    public class when_comparing_should_equal_for_unequal_objects_with_ienumerable_of_compex_type
    {
        static TypeWithIEnumerable _actual;
        static TypeWithIEnumerable _expected;

        Establish context = () =>
            {
                _expected = new TypeWithIEnumerable
                                {
                                    Objects = new List<ComplexType>
                                                  {
                                                      new ComplexType
                                                          {
                                                              StringProperty = "test1",
                                                              DecimalProperty = 10.0m,
                                                              TypeWithString = new TypeWithString { StringProperty = "value1"}
                                                          },
                                                           new ComplexType
                                                          {
                                                              StringProperty = "test1",
                                                              DecimalProperty = 10.0m,
                                                              TypeWithString = new TypeWithString { StringProperty = "value1"}
                                                          }
                                                  }
                                };

                _actual = new TypeWithIEnumerable
                              {
                                  Objects = new List<ComplexType>
                                                {
                                                    new ComplexType
                                                        {
                                                            StringProperty = "test2",
                                                            DecimalProperty = 11.0m,
                                                            TypeWithString = new TypeWithString { StringProperty = "value2"}
                                                        },
                                                         new ComplexType
                                                        {
                                                            StringProperty = "test2",
                                                            DecimalProperty = 11.0m,
                                                            TypeWithString = new TypeWithString { StringProperty = "value2"}
                                                        }
                                                }
                              };
            };
        
        It should_not_be_equal = () => _expected.ToExpectedObject().ShouldNotEqual(_actual);
    }
}