using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using NCommons.Rules.Properties;

namespace NCommons.Rules
{
    public class CommandProcessor<T> : ICommandProcessor
    {
        public IEnumerable<ReturnValue> Process(object message, IMissingCommandStrategy missingCommandStrategy)
        {
            Contract.Assert(typeof (T).IsInstanceOfType(message));
            if (!typeof(T).IsInstanceOfType(message))
                throw new ArgumentException(string.Format(Resources.InvalidMessageType, "message"));

            IList<ReturnValue> returnValues = new List<ReturnValue>();

            IEnumerable<ICommand<T>> commands = ServiceLocator.Current.GetAllInstances<ICommand<T>>();

            if (commands == null || commands.Count() == 0)
            {
                missingCommandStrategy.Execute(message);
            }
            else
            {
                foreach (var command in commands)
                {
                    ReturnValue returnValue = command.Execute((T) message);
                    if (returnValue != null)
                        returnValues.Add(returnValue);
                }
            }

            return returnValues;
        }
    }
}