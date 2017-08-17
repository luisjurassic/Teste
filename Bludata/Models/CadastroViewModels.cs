using System;
using System.ComponentModel.DataAnnotations;

namespace Bludata.Models
{
    public class CadastroViewModels
    {
        [Display(Name = "Cód Cadastro")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório.")]
        [Display(Name = "Nome Completo")]
        [StringLength(300, ErrorMessage = "O Nome deve conter entre 4 a 300 caracteres.", MinimumLength = 4)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é de preenchimento obrigatório.")]
        [Display(Name = "CPF")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [StringLength(11, ErrorMessage = "O RG deve ter no máximo 11 caracteres.")]
        public string Rg { get; set; }

        [Display(Name = "Data Cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é de preenchimento obrigatório.")]
        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "U.F.")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "O Telefone é de preenchimento obrigatório.")]
        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "O Telefone deve conter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

    }
}