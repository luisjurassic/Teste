using System;
using System.ComponentModel.DataAnnotations;

namespace Bludata.Models
{
    public class RelatorioViewModels
    {
        [Display(Name = "Nome")]
        public string FiltroNome { get; set; }

        [Display(Name = "Data Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime? FiltroDCad { get; set; }

        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime? FiltroDNasc { get; set; }
    }
}