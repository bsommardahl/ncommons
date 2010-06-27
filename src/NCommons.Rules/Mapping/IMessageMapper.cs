using System;

namespace NCommons.Rules.Mapping
{
    public interface IMessageMapper
    {
        object Map(object message, Type sourceMessageType, Type destinationMessageType);
        
    }
}