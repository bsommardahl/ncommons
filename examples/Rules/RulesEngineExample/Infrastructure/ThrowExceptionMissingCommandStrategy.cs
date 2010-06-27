using System;
using NCommons.Rules;

namespace RulesEngineExample.Infrastructure
{
    public class ThrowExceptionMissingCommandStrategy : IMissingCommandStrategy
    {
        public void Execute(object message)
        {
            throw new Exception(string.Format("No command was found for message of type {0}.", message.GetType().Name));
        }
    }
}