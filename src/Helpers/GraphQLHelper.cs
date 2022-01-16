using System;
using System.Collections.Generic;
using System.Linq;
using EFTHelper.Extensions;

namespace EFTHelper.Helpers
{
    public static class GraphQLHelper
    {
        #region Fields
        private static Type[] PrimitiveTypes
        {
            get
            {
                return new Type[] 
                { 
                    typeof(int),
                    typeof(long),
                    typeof(bool),
                    typeof(Int32),
                    typeof(Int64),
                    typeof(byte),
                    typeof(sbyte),
                    typeof(Int16),
                    typeof(Boolean),
                    typeof(UInt16),
                    typeof(UInt32),
                    typeof(UInt64),
                    typeof(IntPtr),
                    typeof(UIntPtr),
                    typeof(char),
                    typeof(double),
                    typeof(float),
                    typeof(Single),
                    typeof(string)
                };
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Transforms the object into a GraphQL list of fields to be returned by the api.
        /// </summary>
        public static string SerializeToGraphQL(object request)
        {
            var value = "{";

            var properties = request.GetType().GetProperties();
            foreach (var prop in properties)
            {
                value += $"{prop.Name.FirstCharToLower()} ";
                var type = prop.PropertyType;
                var subProperties = prop.GetType().GetProperties();

                type = HandleEnumerables(type);
                
                if (subProperties.Any() && !PrimitiveTypes.Contains(type))
                {
                    value += SerializeToGraphQL(GetInstance(type));
                }
            }

            value += "}";

            return value;
        }

        /// <summary>
        /// Gets the type of object that is contained in an Enumerable.
        /// </summary>
        private static Type HandleEnumerables(Type type)
        {
            Type[] interfaces = type.GetInterfaces();

            foreach (Type i in interfaces)
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
                {
                    type = i.GetGenericArguments()[0];

                }
            }

            return type;
        }

        private static object GetInstance(Type type)
        {           
            if (PrimitiveTypes.Contains(type))
            {
                return type == typeof(string) ? string.Empty : 0;
            }

            return Activator.CreateInstance(type);
        }
        
        #endregion
    }
}
