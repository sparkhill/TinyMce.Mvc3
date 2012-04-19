using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaPickerService;

namespace TinyMce.Mvc3.Controllers
{
    public class MediaPickerController : Controller
    {
        private readonly IMediaService _mediaService;

        public MediaPickerController() : this(new MediaService())
        {
            
        }

        public MediaPickerController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public Action Index()
        {
            return null;
        }

        public JsonResult GetImages(string path)
        {
            var obj = _mediaService.GetImageTransport(path);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
