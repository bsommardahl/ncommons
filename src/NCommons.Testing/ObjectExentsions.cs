namespace NCommons.Testing
{
    public static class ObjectExentsions
    {
        public static T As<T>(this object value)
        {
            return (T) value;
        }
    }
}