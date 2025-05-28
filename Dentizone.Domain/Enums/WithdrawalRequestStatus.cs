namespace Dentizone.Domain.Enums
{
    public enum WithdrawalRequestStatus
    {
        Pending,   // Request has been made but not yet processed
        Approved,  // Request has been approved and is being processed
        Rejected,  // Request has been rejected
        Completed, // Withdrawal has been successfully completed
        Failed     // Withdrawal failed due to some error
    }
}