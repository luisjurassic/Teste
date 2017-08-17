using Bludata.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using System.Web.Mvc;

namespace Bludata.Extension
{
    public static class Mensagem
    {
        public static void MensagemInformacao(this ControllerBase controller, string mensagem)
        {
            AdicionarMensagem(controller, mensagem, TipoMensagem.Informacao);
        }

        public static void MensagemAlerta(this ControllerBase controller, string mensagem)
        {
            AdicionarMensagem(controller, mensagem, TipoMensagem.Alerta);
        }

        public static void MensagemSucesso(this ControllerBase controller, string mensagem)
        {
            AdicionarMensagem(controller, mensagem, TipoMensagem.Successo);
        }

        public static void MensagemErro(this ControllerBase controller, string mensagem)
        {
            AdicionarMensagem(controller, mensagem, TipoMensagem.Erro);
        }

        public static void MensagemErro(this ControllerBase controller, Exception ex)
        {
            AdicionarMensagem(controller, ObterTodasExcecoes(ex), TipoMensagem.Erro);
        }

        private static string ObterTodasExcecoes(Exception exception)
        {
            StringBuilder sBuilder = new StringBuilder();

            if (exception is DbEntityValidationException)
            {
                var e = exception as DbEntityValidationException;
                foreach (var eve in e.EntityValidationErrors)
                {
                    sBuilder.AppendLine("Descrição: " + $"A entidade do tipo '{ eve.Entry.Entity.GetType().Name }' no estado '{ eve.Entry.State }' tem os seguintes erros de validação:");
                    foreach (var erros in eve.ValidationErrors)
                        sBuilder.AppendLine($"Propriedade : '{erros.PropertyName }', Erro: '{ erros.ErrorMessage }'");
                }
                return sBuilder.ToString();
            }
            else
            {
                Exception ex = exception;

                while (ex != null)
                {
                    sBuilder.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }

                return sBuilder.ToString();
            }
        }

        private static void AdicionarMensagem(this ControllerBase controller, string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            string nomeTipo = tipo.ToString();
            ICollection<string> Colecao = controller.TempData[nomeTipo] as ICollection<string>;

            if (Colecao == null)
                controller.TempData[nomeTipo] = (Colecao = new HashSet<string>());

            Colecao.Add(mensagem);
        }

        public static IEnumerable<string> BuscarMensagem(this HtmlHelper helper, TipoMensagem tipo)
        {
            return helper.ViewContext.Controller.TempData[tipo.ToString()] as ICollection<string> ?? null;
        }
    }
}