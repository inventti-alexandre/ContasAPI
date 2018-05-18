using System;

namespace Contas.Domain.Contas.Commands
{
    public class AtualizarContaCommand : BaseContaCommand
    {
        public AtualizarContaCommand(Guid id, string nome, DateTime data, decimal valor, bool parcelado, int numeroParcelas, string observacao, Guid idCategoria, Guid idUsuario)
        {
            Id = id;
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