using System;
using System.ComponentModel.DataAnnotations;
using Contas.Services.Api.Utils.ValidationUtils;

namespace Contas.Services.Api.ViewModels
{
    public class ContaViewModel
    {
        public ContaViewModel()
        {
            Id = Guid.NewGuid();
            Categoria = new CategoriaViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        public string Nome { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Informe a data")]
        public DateTime Data { get; set; }

        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        [Required(ErrorMessage = "Informe o valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Foi parcelada?")]
        public bool Parcelado { get; set; }

        [Display(Name = "Número de parcelas")]
        [Required(ErrorMessage = "Informe o número de parcelas")]
        public int NumeroParcelas { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [NotEmptyGuid(ErrorMessage = "Informe a categoria")]
        public Guid IdCategoria { get; set; }

        [NotEmptyGuid(ErrorMessage = "Informe o usuário")]
        public Guid IdUsuario { get; set; }

        public CategoriaViewModel Categoria { get; set; }
    }
}