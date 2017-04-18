using System;
using System.Collections.Generic;
using System.Reflection;

namespace ModFx.Extensions
{
    public static class ObjectExtensions
    {
        private const BindingFlags propertyBindingFlags =
            BindingFlags.Instance
            | BindingFlags.Static
            | BindingFlags.Public
            | BindingFlags.IgnoreCase;

        public static object GetValue(this object @object, string fullPath)
        {
            var cObj = @object;

            foreach (var path in fullPath.Split('.'))
            {
                if (cObj == null)
                    return null;

                var asDict = cObj as IDictionary<string, object>;
                if (asDict != null)
                {
                    if (!asDict.ContainsKey(path))
                        return null;

                    cObj = asDict[path];
                    continue;
                }

                var pInfo = GetProperty(cObj, path);

                if (pInfo == null)
                    return null;

                cObj = pInfo.GetValue(cObj);
            }

            return cObj;
        }

        public static void SetValue(this object @object, string property, object value)
        {
            var pInfo = GetProperty(@object, property);
            if (pInfo == null)
                throw new ArgumentException($"Property {property} not found on {@object.GetType().FullName}.");

            pInfo.SetValue(@object, value);
        }

        private static PropertyInfo GetProperty(object resource, string property)
        {
            return resource
                .GetType()
                .GetTypeInfo()
                .GetProperty(property, propertyBindingFlags);
        }
    }
}