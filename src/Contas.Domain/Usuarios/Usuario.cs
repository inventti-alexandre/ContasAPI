using System;
using System.Collections.Generic;
using Contas.Domain.Contas;
using Contas.Domain.Core.Models;

namespace Contas.Domain.Usuarios
{
    public class Usuario : Entity<Usuario>
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        // Propriedades do EntityFramework
        public virtual ICollection<Conta> Contas { get; set; }

        //Construtor para a factory
        private Usuario() { }

        public override bool IsValid()
        {
            return true;
        }

        public static class UsuarioFactory
        {
            public static Usuario NovoUsuario(Guid id, string nome, string sobrenome, DateTime dataNascimento, string cpf, string email)
            {
                var usuario = new Usuario()
                {
                    Id = id,
                    Nome = nome,
                    Sobrenome = sobrenome,
                    DataNascimento = dataNascimento,
                    CPF = cpf,
                    Email = email
                };

                return usuario;
            }
        }
    }
}