namespace Dentizone.Application.DTOs.Withdrawal
{
    public class WithdrawalRequestView
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string WalletId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }

        public string AdminNotes { get; set; }
    }
}