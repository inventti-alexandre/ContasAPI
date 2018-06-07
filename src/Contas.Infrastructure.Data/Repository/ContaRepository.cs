using System;
using System.Collections.Generic;
using System.Linq;
using Contas.Domain.Contas;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Usuarios;
using Contas.Infrastructure.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repository
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(ContasContext context) : base(context)
        {
        }

        public override Conta ObterPorId(Guid id)
        {
            var sql = "SELECT * FROM  Contas c JOIN Categorias ca ON ca.Id = c.IdCategoria WHERE c.Id = @uid";
            var conta = Db.Database.GetDbConnection().Query<Conta, Categoria, Conta>(sql, (c, cat) =>
            {
                c.AtribuirCategoria(cat);
                return c;
            }, new { uid = id });
            return conta.FirstOrDefault();
        }

        public IEnumerable<Conta> ObterContasPorUsuario(Guid id)
        {
            var sql = "SELECT * FROM  Contas c JOIN Categorias ca ON ca.Id = c.IdCategoria WHERE c.Desativado = 0 AND MONTH(c.Data) = MONTH(GETDATE()) AND YEAR(c.Data) = YEAR(GETDATE()) AND C.IdUsuario = @uid ORDER BY DATA DESC";
            var contas = Db.Database.GetDbConnection().Query<Conta, Categoria, Conta>(sql, (conta, categoria) =>
            {
                conta.AtribuirCategoria(categoria);
                return conta;
            }, new { uid = id });
            return contas;
        }

        public IEnumerable<Categoria> ObterCategorias()
        {
            var sql = "SELECT * FROM CATEGORIAS";
            return Db.Database.GetDbConnection().Query<Categoria>(sql);
        }

        public override void Remover(Guid id)
        {
            var conta = ObterPorId(id);
            conta.DesativarConta();
            Atualizar(conta);
        }

        public DateTime ObterDataMaisAntiga()
        {
            var sql = "SELECT MIN(Data) AS data FROM Contas WHERE Desativado = 0;";
            return Db.Database.GetDbConnection().Query<DateTime>(sql).FirstOrDefault();
        }
    }
}