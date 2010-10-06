using System.Text;

namespace NCommons.Specifications
{
    public class ErrorLog<T> : IErrorLog
    {
        readonly StringBuilder _errors = new StringBuilder();
        public static IErrorLog Empty = new NullErrorLog<T>();

        public bool HasErrors
        {
            get { return _errors.Length > 0; }
        }

        public void Write(string error)
        {
            _errors.Append(error);
        }

        public override string ToString()
        {
            return _errors.ToString();
        }
    }
}