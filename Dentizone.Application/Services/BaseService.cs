// Dentizone.Application/Services/BaseService.cs

using System.Security.Claims;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

public abstract class BaseService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected BaseService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Checks if the current user has the ADMIN role.
    /// </summary>
    protected bool IsAdmin()
    {
        var userRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        return userRole == UserRoles.ADMIN.ToString();
    }

    /// <summary>
    /// Ensures the current user is either an Admin or the owner of the specified resource.
    /// Throws UnauthorizedAccessException if the check fails.
    /// </summary>
    /// <param name="resourceId">The unique identifier of the resource to check.</param>
    protected async Task AuthorizeAdminOrOwnerAsync(string resourceId)
    {
        // Admins are always authorized.
        if (IsAdmin())
        {
            return;
        }

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(currentUserId))
        {
            throw new UnauthorizedAccessException("Cannot verify user. No user is authenticated.");
        }

        // Get the owner ID from the concrete service implementation.
        var ownerId = await GetOwnerIdAsync(resourceId);

        if (string.IsNullOrEmpty(ownerId))
        {
            throw new NotFoundException("Could not determine the owner of the resource.");
        }

        // If the user is not the owner, they are not authorized.
        if (ownerId != currentUserId)
        {
            throw new UnauthorizedAccessException(
                "You do not have permission to perform this action on this resource.");
        }
    }

    /// <summary>
    /// When implemented in a derived class, this method retrieves the owner's ID for a given resource.
    /// </summary>
    /// <param name="resourceId">The ID of the resource.</param>
    /// <returns>A Task that represents the asynchronous operation, containing the owner's user ID.</returns>
    protected abstract Task<string> GetOwnerIdAsync(string resourceId);
}