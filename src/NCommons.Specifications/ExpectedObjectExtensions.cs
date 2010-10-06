using System;

namespace NCommons.Specifications
{
    public static class ExpectedObjectExtensions
    {
        public static void MyShouldEqual<T>(this ExpectedObject<T> expectedObject, T actualValue)
        {
            var errorLog = new ErrorLog<T>();
            expectedObject.Equals(actualValue, errorLog, string.Empty);

            if (errorLog.HasErrors)
                throw new Exception(errorLog.ToString());
        }
    }
}