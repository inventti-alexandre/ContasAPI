using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Contas.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public SelectList Categorias()
        {
            return new SelectList(ListaCategoria(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListaCategoria()
        {
            var categoriaList = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel() {Id = new Guid("3b1729ce-4c2b-4be7-9c7f-f2d86be4dc01"), Nome = "Gastos essenciais", Descricao = "São aqueles que não podem faltar, como moradia, contas residenciais, transporte, saúde, educação e mercado."},
                new CategoriaViewModel() {Id = new Guid("f8f4c34f-2fd4-4c2a-a84a-ba2b5a2f2d8d"), Nome = "Receita", Descricao = "São suas receitas, como salários, bônus, rendimentos em aplicações e outras rendas extras."},
                new CategoriaViewModel() {Id = new Guid("c939c9d5-70e5-4553-9a31-882d565f45ea"), Nome = "Estilo de vida", Descricao = "São os gastos das demais categorias que complementam um planejamento pessoal, por exemplo, bares e restaurantes, lazer, compras, viagens etc."},
                new CategoriaViewModel() {Id = new Guid("05cacc06-0bcd-4ccd-9d0e-10feba8bfeeb"), Nome = "Lançamentos entre contas", Descricao = "São transferências entre suas contas e cartões pessoais."}
            };

            return categoriaList;
        }
    }
}