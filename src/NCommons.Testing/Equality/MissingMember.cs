using System;

namespace NCommons.Testing.Equality
{
    public interface IMissingMember
    {
    }

    public class MissingMember<T> : IMissingMember
    {
        public Type MemberType { get { return typeof (T); } }
    }
}