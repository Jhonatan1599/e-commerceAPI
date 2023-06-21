using System.Security.Claims;

namespace API.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPricipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}