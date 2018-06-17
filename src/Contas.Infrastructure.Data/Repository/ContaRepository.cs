using Contas.Domain.Contas;
using Contas.Domain.Contas.Repository;
using Contas.Infrastructure.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Conta> ObterContasPorUsuario(Guid id, DateTime? data, string key)
        {
            var query = DbSet.Where(c => c.IdUsuario == id).Join(Db.Categoria, conta => conta.IdCategoria, categoria => categoria.Id, (conta, categoria) => Conta.ContaFactory.AtribuirCategoria(conta, categoria));
            
            if (data != null) query = query.Where(c => c.Data.Year == data.Value.Year && c.Data.Month == data.Value.Month);
            if (key != null) query = query.Where(c => c.Nome.ToLower().Contains(key.ToLower()));

            return query.ToList();
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

        public DateTime? ObterDataMaisAntiga()
        {
            var sql = "SELECT MIN(Data) AS data FROM Contas WHERE Desativado = 0;";
            return Db.Database.GetDbConnection().Query<DateTime?>(sql).FirstOrDefault();
        }
    }
}