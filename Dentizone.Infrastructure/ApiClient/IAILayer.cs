using Refit;
using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient
{
    public interface IAILayer
    {
        [Get("/all")]
        Task<ApiResponse<ScanAllResponse>> ScanAll([AliasAs("text")] string text);
        [Get("/ contact - toxic")]
        Task<ApiResponse<ScanAllResponse>> ScanContactToxic([AliasAs("text")] string text);
    }

    public class ScanAllResponse
    {
        [JsonPropertyName("contact_info")] public ContactInfo ContactInfo { get; set; } = new();

        [JsonPropertyName("error")] public string? Error { get; set; }

        [JsonPropertyName("is_insult")] public bool IsInsult { get; set; }

        [JsonPropertyName("sentiment")] public string? Sentiment { get; set; }
    }

    public class PhoneNumberInfo
    {
        [JsonPropertyName("value")] public string Value { get; set; } = string.Empty;

        [JsonPropertyName("isValid")] public bool IsValid { get; set; }

        [JsonPropertyName("issue")] public string? Issue { get; set; }
    }

    public class ContactInfo
    {
        [JsonPropertyName("emails")] public List<string> Emails { get; set; } = new();

        [JsonPropertyName("phone_numbers")] public List<PhoneNumberInfo> PhoneNumbers { get; set; } = new();

        [JsonPropertyName("addresses")] public List<string> Addresses { get; set; } = new();
    }
}