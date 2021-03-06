﻿using System;
using System.Collections.Generic;
using Contas.Domain.Contracts;

namespace Contas.Domain.Contas.Repository
{
    public interface IContaRepository : IRepository<Conta>
    {
        IEnumerable<Conta> ObterContasPorUsuario(Guid id, DateTime? data, string key);
        IEnumerable<Categoria> ObterCategorias();
        DateTime? ObterDataMaisAntiga();
    }
}