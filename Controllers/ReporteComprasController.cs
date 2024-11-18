using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContabilidadAltosDelAbejonal.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

public class ReporteComprasController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index()
    {
        return View();
    }

}




