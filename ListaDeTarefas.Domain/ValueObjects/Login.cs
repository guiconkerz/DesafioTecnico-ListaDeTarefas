using ListaDeTarefas.Shared.ValueObjects;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Login : ValueObject
    {
        public string Username { get; private set; }
        public Login(string username) => Username = username.Trim();
        protected Login() { }

    }
}
