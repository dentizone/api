using System.Text.Json.Serialization;

namespace Dentizone.Infrastructure.ApiClient.KYC;

public partial class IdVerification
{
    [JsonPropertyName("status")] public required string Status { get; set; }

    [JsonPropertyName("document_type")] public required string DocumentType { get; set; }

    [JsonPropertyName("document_number")] public required string DocumentNumber { get; set; }

    [JsonPropertyName("personal_number")] public required string PersonalNumber { get; set; }

    [JsonPropertyName("portrait_image")] public required Uri PortraitImage { get; set; }

    [JsonPropertyName("front_image")] public required Uri FrontImage { get; set; }

    [JsonPropertyName("front_video")] public required Uri FrontVideo { get; set; }

    [JsonPropertyName("back_image")] public required Uri BackImage { get; set; }

    [JsonPropertyName("back_video")] public required Uri BackVideo { get; set; }

    [JsonPropertyName("full_front_image")] public required Uri FullFrontImage { get; set; }

    [JsonPropertyName("full_back_image")] public required Uri FullBackImage { get; set; }

    [JsonPropertyName("date_of_birth")] public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("age")] public long Age { get; set; }

    [JsonPropertyName("expiration_date")] public DateTimeOffset ExpirationDate { get; set; }

    [JsonPropertyName("date_of_issue")] public DateTimeOffset DateOfIssue { get; set; }

    [JsonPropertyName("issuing_state")] public required string IssuingState { get; set; }


    [JsonPropertyName("full_name")] public required string FullName { get; set; }


    [JsonPropertyName("address")] public required string Address { get; set; }

    [JsonPropertyName("place_of_birth")] public required string PlaceOfBirth { get; set; }


    [JsonPropertyName("nationality")] public required string Nationality { get; set; }
}