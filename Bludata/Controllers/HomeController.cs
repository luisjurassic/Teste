using Bludata.EF;
using Bludata.Enum;
using Bludata.Extension;
using Bludata.Filter;
using Bludata.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bludata.Controllers
{
    [SistemaUF]
    public class HomeController : Controller
    {
        IEnumerable<CadastroViewModels> ListaPessoas { get; set; }
        private Entities db;
        public HomeController()
        {
            db = new Entities();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Descrição do Sistema.";
            return View();
        }

        public ActionResult Contato()
        {
            ViewBag.Message = "Onde me encontrar?";
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(CadastroViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Pessoas pe = new Pessoas()
                    {
                        Cpf = Helper.RemoverCaracteres(model.Cpf),
                        Rg = Helper.RemoverCaracteres(model.Rg),
                        DataCadastro = DateTime.Now,
                        DataNascimento = Convert.ToDateTime(model.DataNascimento),
                        Nome = model.Nome,
                        Uf = Session["RodandoEstado"].ToString(),
                        Telefone = Helper.RemoverCaracteres(model.Telefone)
                    };

                    db.Pessoas.Add(pe);
                    db.SaveChanges();
                    this.MensagemSucesso(pe.Nome + " salvo com sucesso!");
                    return RedirectToAction("Cadastro");
                }
                catch (Exception ex)
                {
                    this.MensagemErro(ex);
                }
            }
            return View();
        }

        ~HomeController()
        {
            db.Dispose();
        }
    }
}