using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models.TarkovTools;
using EFTHelper.Models.TarkovTools.Requests;
using EFTHelper.Models.TarkovTools.Responses;

namespace EFTHelper.Services
{
    public class TarkovToolsService : HttpServiceBase
    {
        #region Fields

        private const string apiUrl = "https://tarkov-tools.com/";
        private const string apiEndpoint = "graphql";

        #endregion

        #region Constructors

        public TarkovToolsService() : base()
        {
            _httpClient.BaseAddress = new Uri(apiUrl);
        }

        #endregion

        #region Methods

        public Task<ItemsByNameResponse> GetItemsByNameAsync(string name)
        {
            var request = new GraphQLRequest(Enums.TarkovToolsRequestTypes.ItemsByName, new ItemBaseRequest(), name);

            return ExecutePostRequestAsync(request, new ItemsByNameResponse());
        }

        public Task<ItemsByTypeResponse> GetItemsByTypeAsync(ItemTypes itemType)
        {
            var request = new GraphQLRequest(TarkovToolsRequestTypes.ItemsByType, new ItemBaseRequest(), itemType.ToString().FirstCharToLower());

            return ExecutePostRequestAsync(request, new ItemsByTypeResponse());
        }

        public Task<ItemsByTypeResponse> GetAllItemsAsync()
        {
            return GetItemsByTypeAsync(ItemTypes.Any);
        }       

        public Task<ItemByIdResponse> GetItemByIdAsync(string id)
        {
            var request = new GraphQLRequest(TarkovToolsRequestTypes.Item, new ItemRequest(), id);

            return ExecutePostRequestAsync(request, new ItemByIdResponse());
        }

        private async Task<TResponse> ExecutePostRequestAsync<TResponse>(GraphQLRequest request, TResponse defaultValue)
        {
            var response = await ExecutePostRequestAsync<GraphQLRequest, TarkovToolsResponse>(apiEndpoint, request);

            return TryDeserialize(response.Data.ToString(), defaultValue);
        }

        #endregion
    }
}
