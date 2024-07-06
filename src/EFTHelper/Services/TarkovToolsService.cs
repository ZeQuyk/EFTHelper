using System;
using System.Threading.Tasks;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models.TarkovTools;
using EFTHelper.Models.TarkovTools.Requests;
using EFTHelper.Models.TarkovTools.Responses;

namespace EFTHelper.Services;

public class TarkovToolsService : HttpServiceBase
{
    #region Fields

    private const string ApiUrl = "https://api.tarkov.dev/";
    private const string ApiEndpoint = "graphql";

    #endregion

    #region Constructors

    public TarkovToolsService()
    {
        HttpClient.BaseAddress = new Uri(ApiUrl);
    }

    #endregion

    #region Methods

    public Task<ItemsByNameResponse<TItem>> GetItemsByNameAsync<TItem>(string name)
        where TItem : ItemBase, new()
    {
        var request = new GraphQLRequest<TItem>(TarkovToolsRequestTypes.ItemsByName, name);

        return ExecutePostRequestAsync(request, new ItemsByNameResponse<TItem>());
    }

    public Task<ItemsByTypeResponse> GetItemsByTypeAsync(ItemTypes itemType)
    {
        var request = new GraphQLRequest<ItemBaseRequest>(TarkovToolsRequestTypes.ItemsByType, itemType.ToString().FirstCharToLower());

        return ExecutePostRequestAsync(request, new ItemsByTypeResponse());
    }

    public Task<ItemByIdResponse> GetItemByIdAsync(string id)
    {
        var request = new GraphQLRequest<ItemRequest>(TarkovToolsRequestTypes.Item, id);

        return ExecutePostRequestAsync(request, new ItemByIdResponse());
    }

    public Task<QuestsResponse> GetEFTTasks()
    {
        var request = new GraphQLRequest<QuestsRequest>(TarkovToolsRequestTypes.Quests, string.Empty);

        return ExecutePostRequestAsync(request, new QuestsResponse());
    }

    private async Task<TResponse> ExecutePostRequestAsync<TResponse>(IGraphQLRequest request, TResponse defaultValue)
    {
        var response = await ExecutePostRequestAsync<IGraphQLRequest, TarkovToolsResponse>(ApiEndpoint, request);

        return TryDeserialize(response?.Data?.ToString(), defaultValue);
    }

    #endregion
}