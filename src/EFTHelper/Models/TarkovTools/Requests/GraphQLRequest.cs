using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Helpers;

namespace EFTHelper.Models.TarkovTools.Requests;

public class GraphQLRequest<T> : IGraphQLRequest
    where T : class
{
    #region Constructors

    public GraphQLRequest(TarkovToolsRequestTypes requestType, string filterValue)
    {
        Query = BuildQuery(requestType, filterValue);
    }

    #endregion

    #region Properties

    public string Query { get; }

    #endregion

    #region Methods

    private static string BuildQuery(TarkovToolsRequestTypes requestType, string filterValue)
    {
        var query = "{ ";
        var filterName = requestType.AssociatedFilterName();

        query += $"{requestType.ToString().FirstCharToLower()} ";

        if (!string.IsNullOrEmpty(filterName))
        {
            var filter = requestType == TarkovToolsRequestTypes.ItemsByType ? filterValue : $"\"{filterValue}\"";
            query += $"({filterName}: {filter}) ";
        }

        query += GraphQLHelper.SerializeToGraphQL<T>();
        query += "}";

        return query;
    }

    #endregion
}
