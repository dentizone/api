using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient.KYC
{
    public class CreateSessionRequest
    {
        [JsonPropertyName("workflow_id")] public string WorkflowId { get; set; }

        [JsonPropertyName("vendor_data")] public string VendorData { get; set; }

        [JsonPropertyName("callback")] public string Callback { get; set; }

        [JsonPropertyName("metadata")] public string Metadata { get; set; }

        [JsonPropertyName("contact_details")] public ContactDetails ContactDetails { get; set; }

        [JsonPropertyName("expected_details")] public ExpectedDetails ExpectedDetails { get; set; }
    }

    public class ContactDetails
    {
        [JsonPropertyName("email")] public string Email { get; set; }
    }

    public class ExpectedDetails
    {
        [JsonPropertyName("first_name")] public string FirstName { get; set; }

        [JsonPropertyName("last_name")] public string LastName { get; set; }

        [JsonPropertyName("country")] public string Country { get; set; }
    }

    public class CreateSessionResponse
    {
        [JsonPropertyName("session_id")] public string SessionId { get; set; }


        [JsonPropertyName("session_token")] public string SessionToken { get; set; }


        [JsonPropertyName("status")] public string Status { get; set; }


        [JsonPropertyName("url")] public string Url { get; set; }
    }
}