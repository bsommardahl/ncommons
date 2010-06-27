using System.Web.Mvc;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;
using NCommons.Rules;
using NCommons.Rules.Mapping;
using StructureMap.Configuration.DSL;

namespace RulesEngineExample.Infrastructure
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType(typeof(IRulesEngine));
                    x.AddAllTypesOf<IController>()
                        .NameBy(type => type.Name.Substring(0, type.Name.Length - 10));
                    x.WithDefaultConventions();
                    x.ConnectImplementationsToTypesClosing(typeof (ICommand<>));
                    x.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                    x.ConnectImplementationsToTypesClosing(typeof (AssociationConfiguration<>));
                });

            For<IServiceLocator>().Singleton().Use<StructureMapServiceLocator>();
            For<IControllerFactory>().Use<StructureMapControllerFactory>();
            For<IRulesValidator>().Use<FluentValidationRulesValidator>();
            For<IRulesEngine>().Use<MappingRulesEngine>();
            For<IMessageMapper>().Use<MessageMapper>();
            For<IMissingCommandStrategy>().Use<ThrowExceptionMissingCommandStrategy>();
        }
    }
}