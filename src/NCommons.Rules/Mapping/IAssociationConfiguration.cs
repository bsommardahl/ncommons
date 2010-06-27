using System;

namespace NCommons.Rules.Mapping
{
    public interface IAssociationConfiguration
    {
        Type GetDestinationMessageType();
        string GetSourcePropertyNameFor(string destinationPropertyName);
    }
}