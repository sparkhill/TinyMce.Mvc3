using System.Web.Mvc;
using TinyMce.Mvc3.Models;

namespace TinyMce.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new TestModel();

            model.Content = MvcHtmlString.Create("<img src='http://freeimagesarchive.com/data/media/8/5_images.jpg'/>").ToString();

            return View(model);
        }
    }
}
