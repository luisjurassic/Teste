using System.Configuration;
using System.Net;
using System.Web.Mvc;

namespace Bludata.Filter
{
    public class SistemaUFAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ufSistema = ConfigurationManager.AppSettings["SistemaUf"].ToString().ToUpper();
            if (ufSistema != "PR" && ufSistema != "SC")
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NonAuthoritativeInformation);
            else
                filterContext.HttpContext.Session["RodandoEstado"] = ufSistema;
        }
    }
}