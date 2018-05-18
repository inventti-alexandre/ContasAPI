using System;
using Contas.Domain.Core.Commands;

namespace Contas.Domain.Usuarios.Commands
{
    public class RegistrarUsuarioCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public RegistrarUsuarioCommand(Guid id, string nome, string sobrenome, string cpf, string email, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cpf;
            Email = email;
            DataNascimento = dataNascimento;
        }
    }
}