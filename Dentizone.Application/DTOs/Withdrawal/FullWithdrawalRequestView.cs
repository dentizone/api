namespace Dentizone.Application.DTOs.Withdrawal;

public class FullWithdrawalRequestView : WithdrawalRequestView
{
    public required string UserName { get; set; }
    public required string UserEmail { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}