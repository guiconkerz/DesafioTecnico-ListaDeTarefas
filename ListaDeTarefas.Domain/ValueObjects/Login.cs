using ListaDeTarefas.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Domain.ValueObjects
{
    public sealed class Login : ValueObject
    {
        public string Username { get; private set; }
        public Login(string username) => Username = username;
        protected Login() { }

    }
}
