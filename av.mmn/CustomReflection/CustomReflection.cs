using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomFunctions
{
    /// <summary>
    /// Набор методов для работы с рефлексией.
    /// </summary>
    public class CustomReflection
    {
        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute(Object obj, Type attributeType, Func<Object, bool> WhereAttribute)
        {
            return obj.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(
                        p =>
                            p.GetCustomAttributes(attributeType, false).Any(WhereAttribute));
        }

        public static Object GetValuePropertyByAttribute(Object obj, Type attributeType,
            Func<Object, bool> WhereAttribute)
        {
            return
                obj.GetType()
                    .GetProperty(GetPropertiesByAttribute(obj, attributeType, WhereAttribute).First().Name,
                        BindingFlags.Instance | BindingFlags.Public)
                    .GetValue(obj, null);
        }

        public static Object GetValueFromAttribute(Object obj, Type attributeType, string nameProperty, Func<Object, bool> WhereAttribute)
        {
            return
                obj.GetType()
                    .GetProperty(nameProperty, BindingFlags.Public | BindingFlags.Instance)
                    .GetCustomAttributes(attributeType, true)
                    .Where(WhereAttribute);
            
        }
    }
}
