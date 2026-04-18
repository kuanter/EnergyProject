using EnergyProject.Application.Interfaces.Stuff;
using System.Security.Claims;

namespace EnergyProject.Application.Services.Stuff
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        private string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);


        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string GetRequiredUserId()
        {
            var id = UserId;
            if (string.IsNullOrEmpty(id))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return id;
        }

    }
}
