namespace Dentizone.Domain.Enums
{
    public enum UserRoles
    {
        Ghost,           // Registered but not verified (No Email Verification or KYC)
        PartilyVerified, // Registered and JUST mail Verified
        Verified,        // Registered and KYC Verified
        Blacklisted,     // Blocked from our system
        Admin            // Full access
    }
}