using System.Text.Json.Serialization;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Helpers;

namespace EFTHelper.Models.TarkovTools.Requests;

public class GraphQLRequest
{
    #region Constructors

    public GraphQLRequest(TarkovToolsRequestTypes requestType, object expectedResponse, string filterValue)
    {
        Query = BuildQuery(requestType, expectedResponse, filterValue);
    }

    #endregion

    #region Properties

    [JsonPropertyName("query")]
    public string Query { get; }

    #endregion

    #region Methods

    private string BuildQuery(TarkovToolsRequestTypes requestType, object request, string filterValue)
    {
        var query = "{ ";
        var filterName = requestType.AssociatedFilterName();

        query += $"{requestType.ToString().FirstCharToLower()} ";

        if (!string.IsNullOrEmpty(filterName))
        {
            var filter = requestType == TarkovToolsRequestTypes.ItemsByType ? filterValue : $"\"{filterValue}\"";
            query += $"({filterName}: {filter}) ";
        }

        query += GraphQLHelper.SerializeToGraphQL(request);
        query += "}";

        return query;
    }

    #endregion
}
