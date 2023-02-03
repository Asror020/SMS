using System.Security.Claims;

namespace SMS.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserId(this ClaimsPrincipal principal) 
            => principal.FindFirstValue("UserId") != null;

        public static long GetUserId(this ClaimsPrincipal principal)
        {
            if (!HasUserId(principal))
                throw new Exception();
            
            return Convert.ToInt64(principal.FindFirstValue("UserId"));
        }
    }
}
