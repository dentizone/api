namespace Dentizone.Domain.Enums
{
    public enum UserState
    {
        PendingVerification, // Registered but not verified (Ghost)
        EmailVerified, // Registered and email verified (Partily Verified)
        Active, // Active user (KYC Approved)
        Blacklisted, // Blocked from our system
        Deleted // Deleted user
    }
}