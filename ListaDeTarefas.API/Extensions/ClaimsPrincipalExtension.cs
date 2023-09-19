using System.Security.Claims;

namespace PesquisaSatisfacao.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Name(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;

        public static string Email(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }
}
