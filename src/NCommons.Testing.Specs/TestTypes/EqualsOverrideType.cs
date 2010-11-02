namespace NCommons.Testing.Specs.TestTypes
{
    public class EqualsOverrideType
    {
        readonly bool _isEqual;

        public EqualsOverrideType(bool isEqual)
        {
            _isEqual = isEqual;
        }

        public override bool Equals(object obj)
        {
            return _isEqual;
        }
    }
}