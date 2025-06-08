using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient.KYC;

public partial class IdVerification
{
    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("document_type")] public string DocumentType { get; set; }

    [JsonPropertyName("document_number")] public string DocumentNumber { get; set; }

    [JsonPropertyName("personal_number")] public string PersonalNumber { get; set; }

    [JsonPropertyName("portrait_image")] public Uri PortraitImage { get; set; }

    [JsonPropertyName("front_image")] public Uri FrontImage { get; set; }

    [JsonPropertyName("front_video")] public Uri FrontVideo { get; set; }

    [JsonPropertyName("back_image")] public Uri BackImage { get; set; }

    [JsonPropertyName("back_video")] public Uri BackVideo { get; set; }

    [JsonPropertyName("full_front_image")] public Uri FullFrontImage { get; set; }

    [JsonPropertyName("full_back_image")] public Uri FullBackImage { get; set; }

    [JsonPropertyName("date_of_birth")] public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("age")] public long Age { get; set; }

    [JsonPropertyName("expiration_date")] public DateTimeOffset ExpirationDate { get; set; }

    [JsonPropertyName("date_of_issue")] public DateTimeOffset DateOfIssue { get; set; }

    [JsonPropertyName("issuing_state")] public string IssuingState { get; set; }


    [JsonPropertyName("full_name")] public string FullName { get; set; }


    [JsonPropertyName("address")] public string Address { get; set; }

    [JsonPropertyName("place_of_birth")] public string PlaceOfBirth { get; set; }


    [JsonPropertyName("nationality")] public string Nationality { get; set; }
}