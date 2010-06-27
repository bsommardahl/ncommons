using System.Linq.Expressions;
using System.Reflection;

namespace NCommons.Rules.Mapping
{
    public static class PropertyInfoHelper
    {
        public static PropertyInfo GetPropertyInfo(Expression expression)
        {
            PropertyInfo propertyInfo = null;

            switch (expression.NodeType)
            {
                case ExpressionType.Convert:

                    var unaryExpression = (UnaryExpression)expression;
                    var memberExpression = (MemberExpression)unaryExpression.Operand;
                    propertyInfo = (PropertyInfo)memberExpression.Member;
                    break;

                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)expression;
                    propertyInfo = (PropertyInfo)memberExpression.Member;
                    break;
            }

            return propertyInfo;
        }
    }
}