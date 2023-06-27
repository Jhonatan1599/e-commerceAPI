using System.Security.Claims;

namespace API.Extentions
{
 
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// An extension method for ClaimsPrincipal
        /// </summary>
        /// <param name="user">ClaimsPrincipal that represent the info about logged in user</param>
        /// <returns>A string that represent the email of the user</returns>
        public static string RetrieveEmailFromPricipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}