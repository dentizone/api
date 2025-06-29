namespace Dentizone.Application.DTOs.Withdrawal
{
    public class WithdrawalRequestView
    {
        public required string Id { get; set; }
        public decimal Amount { get; set; }
        public required string WalletId { get; set; }
        public required string Status { get; set; }
        public required string UserId { get; set; }

        public required string AdminNotes { get; set; }
    }
}