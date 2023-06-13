using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EFTHelper.Extensions;

namespace EFTHelper.Services;

public abstract class HttpServiceBase
{
    #region Constructors

    public HttpServiceBase()
    {
        HttpClient = new HttpClient();
    }

    #endregion

    #region Properties

    protected readonly HttpClient HttpClient;
    
    protected JsonSerializerOptions Options
    {
        get { return GetDefaultOptions(); }
    }

    #endregion

    #region Methods

    protected Task<TResponse> ExecutePostRequestAsync<TRequest, TResponse>(string url, TRequest request)
    {
         return ExecutePostRequestAsync<TRequest,TResponse>(url, request, default);
    }

    protected Task<TResponse> ExecutePostRequestAsync<TRequest, TResponse>(string url, TRequest request, TResponse defaultValue)
    {
        return ExecutePostRequestAsync(url, request, defaultValue, Options);
    }

    protected async Task<TResponse> ExecutePostRequestAsync<TRequest, TResponse>(string url, TRequest request, TResponse defaultValue, JsonSerializerOptions options)
    {
        using var content = new StringContent(JsonSerializer.Serialize(request));

        using var result = await HttpClient.PostAsync(url, content);

        return await HandleResponse(result, defaultValue, options);
    }

    protected Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
    {
        return HandleResponse<TResponse>(response, default);
    }

    protected Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response, TResponse defaultValue)
    {
        return HandleResponse(response, defaultValue, Options);
    }

    protected async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response, TResponse defaultValue, JsonSerializerOptions options)
    {
        if (!response.IsSuccessStatusCode)
        {
            return await Task.FromResult(defaultValue);
        }

        var content = await response.Content.ReadAsStringAsync();

        return TryDeserialize(content, defaultValue, options);
    }

    protected JsonSerializerOptions GetDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.AddCustomConverters();

        return options;
    }

    protected TResponse TryDeserialize<TResponse>(string jsonData, TResponse defaultValue)
    {
        return TryDeserialize(jsonData, defaultValue, Options);
    }

    protected TResponse TryDeserialize<TResponse>(string jsonData, TResponse defaultValue, JsonSerializerOptions options)
    {
        try
        {
            return JsonSerializer.Deserialize<TResponse>(jsonData, options);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion
}
