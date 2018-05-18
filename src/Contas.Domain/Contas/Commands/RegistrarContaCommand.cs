using System;
using Contas.Domain.Usuarios;

namespace Contas.Domain.Contas.Commands
{
    public class RegistrarContaCommand : BaseContaCommand
    {
        public RegistrarContaCommand(string nome, DateTime data, decimal valor, bool parcelado, int numeroParcelas, string observacao, Guid idCategoria, Guid idUsuario)
        {
            Nome = nome;
            Data = data;
            Valor = valor;
            Parcelado = parcelado;
            NumeroParcelas = numeroParcelas;
            Observacao = observacao;
            IdCategoria = idCategoria;
            IdUsuario = idUsuario;
        }
    }
}