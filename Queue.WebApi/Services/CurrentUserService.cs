using Queue.Application.Interfaces;
using System.Security.Claims;

namespace Queue.WebApi.Services
{
    public class CurrentUserService:ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)=>
            _httpContextAccessor = httpContextAccessor;
        public Guid Id { 
            get 
            {
                var id = _httpContextAccessor.HttpContext?.User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            } 
        }
    }
}
