namespace Dentizone.Application.DTOs;

public class ErrorResponse
{
    public required string Message { get; set; }
    public string? Details { get; set; }
    public int StatusCode { get; set; }
}