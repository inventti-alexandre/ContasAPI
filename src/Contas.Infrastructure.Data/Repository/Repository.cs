﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Models;
using Contas.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected ContasContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(ContasContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> @where)
        {
            return DbSet.AsNoTracking().Where(where);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}