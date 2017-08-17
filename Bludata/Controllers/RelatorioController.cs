using Bludata.EF;
using Bludata.Enum;
using Bludata.Extension;
using Bludata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bludata.Controllers
{
    public class RelatorioController : Controller
    {
        private IEnumerable<CadastroViewModels> ListaCadastro;
        private Entities db;
        public RelatorioController()
        {
            db = new Entities();
        }

        public ActionResult Lista(string nome, DateTime? dCad, DateTime? dNasc, string classificacao = "")
        {
            ViewData["ClassNome"] = string.IsNullOrWhiteSpace(classificacao) ? "Nome_Desc" : "";
            ViewData["ClassDataCad"] = classificacao == "DataCad" ? "DataCad_Desc" : "DataCad";
            ViewData["ClassDataNasc"] = classificacao == "DataNasc" ? "DataNasc_Desc" : "DataNasc";
            ViewData["FiltroNome"] = nome;
            ViewData["FiltroDataCad"] = dCad;
            ViewData["FiltroDataNasc"] = dNasc;

            try
            {
                Pesquisar();
                Filtragem(nome, dCad, dNasc);
                Classificar(classificacao);
            }
            catch (Exception ex)
            {
                this.MensagemErro(ex);
            }
            return View(ListaCadastro);
        }

        private void Pesquisar()
        {
            ListaCadastro = (from pe in db.Pessoas
                             select new CadastroViewModels()
                             {
                                 Cpf = pe.Cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-"),
                                 DataCadastro = pe.DataCadastro,
                                 DataNascimento = pe.DataNascimento,
                                 Nome = pe.Nome,
                                 Rg = pe.Rg,
                                 Telefone = pe.Telefone,
                                 Uf = pe.Uf
                             });
        }

        private void Filtragem(string nome, DateTime? dCad, DateTime? dNasc)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                ListaCadastro = ListaCadastro.Where(w => w.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
            if (dCad != null)
                ListaCadastro = ListaCadastro.Where(w => w.DataCadastro.Date == dCad).ToList();
            if (dNasc != null)
                ListaCadastro = ListaCadastro.Where(w => w.DataNascimento.Date == dNasc).ToList();
        }

        private void Classificar(string classficacao)
        {
            switch (classficacao)
            {
                case "Nome_Desc":
                    ListaCadastro = ListaCadastro.OrderByDescending(o => o.Nome).ToList();
                    break;
                case "DataCad":
                    ListaCadastro = ListaCadastro.OrderBy(o => o.DataCadastro).ToList();
                    break;
                case "DataCad_Desc":
                    ListaCadastro = ListaCadastro.OrderByDescending(o => o.DataCadastro).ToList();
                    break;
                case "DataNasc":
                    ListaCadastro = ListaCadastro.OrderBy(o => o.DataNascimento).ToList();
                    break;
                case "DataNasc_Desc":
                    ListaCadastro = ListaCadastro.OrderByDescending(o => o.DataNascimento).ToList();
                    break;
                default:
                    ListaCadastro = ListaCadastro.OrderBy(o => o.Nome).ToList();
                    break;
            }
        }

        ~RelatorioController()
        {
            db.Dispose();
        }
    }
}