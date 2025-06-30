namespace Dentizone.Domain.Enums
{
    public enum UserState
    {
        PendingVerification, // Registered but not verified (Ghost)
        EmailVerified,       // Registered and email verified (PartilyVerified)
        KycVerified,         // Registered and KYC verified (Verified)
        Active,              // Active user (could be KYC verified)
        Blacklisted,         // Blocked from our system
        Deleted              // Deleted user
    }

}
