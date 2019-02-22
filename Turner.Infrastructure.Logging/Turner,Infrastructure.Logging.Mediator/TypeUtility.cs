using System;
using System.Linq;
using System.Reflection;

namespace Turner.Infrastructure.Logging.Mediator
{
    public static class TypeUtility
    {
        public static bool ContainsAttribute(Type type, Type attributeType)
        {
            var customAttribute = type.GetTypeInfo().GetCustomAttribute(attributeType);

            return customAttribute != null;
        }

        public static string GetPrettyName(Type type)
        {
            var prettyName = type.Name;
            if (type.GetTypeInfo().IsGenericType)
            {
                var backtick = prettyName.IndexOf('`');
                if (backtick > 0)
                {
                    prettyName = prettyName.Remove(backtick);
                }
                prettyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (var i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = GetPrettyName(typeParameters[i]);
                    prettyName += (i == 0 ? typeParamName : "," + typeParamName);
                }
                prettyName += ">";
            }
            else
            {
                prettyName = type.FullName.Split('.').Reverse().First();
            }

            return prettyName.Replace('+', '.');
        }
    }
}
