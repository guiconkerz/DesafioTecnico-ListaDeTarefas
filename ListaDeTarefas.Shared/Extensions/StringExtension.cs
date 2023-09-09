using System.Text;

namespace ListaDeTarefas.Shared.Extensions
{
    public static class StringExtension
    {
        public static string ToBase64(this string text) => Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }
}
