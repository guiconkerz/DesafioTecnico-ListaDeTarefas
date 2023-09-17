using System.Security.Claims;

namespace ListaDeTarefas.Infra.Services.Extensions
{
    public static class ClaimTypesExtension
    {
        public static string Email(this ClaimsPrincipal user)
        {
			try
			{
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            }
			catch 
			{
                return string.Empty;
            }
        }

        public static string Login(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
