using System;
using AutoMapper;
using NCommons.Rules.Mapping;

namespace RulesEngineExample.Infrastructure
{
    public class MessageMapper : IMessageMapper
    {
        public object Map(object message, Type sourceMessageType, Type destinationMessageType)
        {
            return Mapper.Map(message, sourceMessageType, destinationMessageType);
        }
    }
}