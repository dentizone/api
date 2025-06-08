using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient.KYC;

public class SessionDecisionResponse
{
    [JsonPropertyName("session_id")] public Guid SessionId { get; set; }

    [JsonPropertyName("session_number")] public long SessionNumber { get; set; }

    [JsonPropertyName("session_url")] public Uri SessionUrl { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("vendor_data")] public Guid VendorData { get; set; }


    [JsonPropertyName("id_verification")] public IdVerification IdVerification { get; set; }
}