using System;
using System.Web;
using System.Web.Mvc;
using MediaPickerService;

namespace TinyMce.Mvc3.Controllers
{
    public class MediaPickerController : Controller
    {
        private readonly IMediaService _mediaService;

        // you can replace this with your own dependency injection
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

        public ActionResult Download(string path)
        {
            var file = _mediaService.GetFile(path);
            var mimeType = _mediaService.GetMimeType(path);
            return File(file, mimeType);
        }

        [HttpPost]
        public JsonResult CreateFolder(string path, string foldername)
        {
            var success = _mediaService.CreateFolder(path, foldername);
            return Json(success);
        }

        [HttpPost]
        public ContentResult UploadFile(string path, HttpPostedFileBase thefile)
        {
            _mediaService.StoreFile(thefile, path);

            var code = "Upload Successful!";

            var script =
            @"
                <script>
                    parent.mediapicker.uploadComplete('{0}');
                </script>
            ";

            script = string.Format(script, code);

            var retval = new MvcHtmlString(script).ToString();
            return Content(retval);
        }
    }
}
