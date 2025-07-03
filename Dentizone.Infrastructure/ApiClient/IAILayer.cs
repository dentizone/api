using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient
{
    public interface IAILayer
    {
        [Get("/all")]
        Task<ApiResponse<ScanAllResponse>> ScanAll([AliasAs("text")] string text);
    }

    public class ScanAllResponse
    {
        [JsonPropertyName("contact_info")] public string ContactInfoRaw { get; set; } = string.Empty;

        [JsonPropertyName("error")] public string? Error { get; set; }

        [JsonPropertyName("is_insult")] public bool IsInsult { get; set; }

        [JsonPropertyName("sentiment")] public string? Sentiment { get; set; }

        // Optional helper to deserialize contact_info
        public ContactInfo? ContactInfo =>
            JsonSerializer.Deserialize<ContactInfo>(ContactInfoRaw ?? "{}");
    }

    public class ContactInfo
    {
        [JsonPropertyName("emails")] public List<string> Emails { get; set; } = new();

        [JsonPropertyName("phone_numbers")] public List<string> PhoneNumbers { get; set; } = new();

        [JsonPropertyName("addresses")] public List<string> Addresses { get; set; } = new();
    }
}