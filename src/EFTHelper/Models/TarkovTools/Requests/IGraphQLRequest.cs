using System.Text.Json.Serialization;

namespace EFTHelper.Models.TarkovTools.Requests;

public interface IGraphQLRequest
{
    [JsonPropertyName("query")]
    string Query { get; }
}
