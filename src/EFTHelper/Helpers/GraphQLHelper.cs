using System;
using System.Collections.Generic;
using System.Linq;
using EFTHelper.Extensions;

namespace EFTHelper.Helpers;

public static class GraphQLHelper
{
    #region Fields

    private static readonly Type[] _primitiveTypes = new Type[]
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


    #endregion

    #region Methods

    /// <summary>
    /// Transforms the object into a GraphQL list of fields to be returned by the api.
    /// </summary>
    public static string SerializeToGraphQL<T>()
        => SerializeToGraphQL(typeof(T));

    private static string SerializeToGraphQL(Type type)
    {
        var value = "{";

        var properties = type.GetProperties();
        foreach (var prop in properties)
        {
            value += $"{prop.Name.FirstCharToLower()} ";
            var propertyType = prop.PropertyType;
            var subProperties = propertyType.GetType().GetProperties();

            propertyType = HandleEnumerables(propertyType);

            if (subProperties.Any() && !_primitiveTypes.Contains(propertyType))
            {
                value += SerializeToGraphQL(propertyType);
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
        var interfaces = type.GetInterfaces();

        foreach (var i in interfaces)
        {
            if (i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
            {
                type = i.GetGenericArguments()[0];

            }
        }

        return type;
    }

    #endregion
}
