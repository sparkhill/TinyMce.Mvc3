using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using MediaPickerService.Dto;
using Microsoft.Win32;

namespace MediaPickerService
{
    public class MediaService : IMediaService
    {
        private string _path;
        private string _root;

        public string ResolveMediaPath(string path)
        {
            var root = ConfigurationManager.AppSettings["MediaDirectory"];

            _path = path;
            _root = root;

            if (path == null)
                path = "";

            // if it starts with a squiggly we need to resolve by the application root
            if (string.IsNullOrEmpty(root))
                return "";

            path = path.Replace(@"//", @"/");

            if (!string.IsNullOrEmpty(path) && path[0] == '/')
                path = path.Substring(1);

            path = path.Replace(@"/", @"\");

            if(root[0] == '~')
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                root = root.Replace("~/", "");
                root = root.Replace(@"/", @"\");
                return Path.Combine(baseDir, root, path);
            }

            return path;
        }

        public Transport GetImageTransport(string path)
        {
            var physical = ResolveMediaPath(path);

            if (physical == "")
                return null;

            // create the mediae list
            var mediae = BuildMediaeList(physical);

            // create the breadcrumbs
            var breadcrumbs = BuildBreadcrumbs();

            // return transport object.
            return new Transport
            {
                Breadcrumbs = breadcrumbs,
                Mediae = mediae
            };
        }

        public byte[] GetFile(string path)
        {
            var fname = ResolveMediaPath(path);
            var file = File.ReadAllBytes(fname);
            return file;
        }

        public string GetMimeType(string path)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(path);

            if (string.IsNullOrEmpty(ext))
                return mimeType;

            ext = ext.ToLower();

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            return mimeType;
        }

        public bool CreateFolder(string path, string foldername)
        {
            try
            {
                var fullpath = ResolveMediaPath(path);
                fullpath = Path.Combine(fullpath, foldername);
                Directory.CreateDirectory(fullpath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StoreFile(HttpPostedFileBase thefile, string path)
        {
            try
            {
                var p = ResolveMediaPath(path);
                var partialFileName = Path.GetFileName(thefile.FileName);
                var fname = Path.Combine(p, partialFileName ?? "unknown");
                thefile.SaveAs(fname);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<Breadcrumb> BuildBreadcrumbs()
        {
            var breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb {Name = "Media", Url = ""}
            };

            var path = _path.Replace("\\", "/");

            var directories = path.Split('/');

            for (int i = 0; i < directories.Length; i++)
            {
                var name = directories[i];
                var url = "";

                if(string.IsNullOrEmpty(name)) continue;

                for (var j = 0; j < i + 1; j++)
                    url += directories[j] + "/";

                url = url.Trim('/');

                var item = new Breadcrumb
                {
                    Name = name,
                    Url = url
                };

                breadcrumbs.Add(item);
            }

            return breadcrumbs;
        }

        private IEnumerable<Media> BuildMediaeList(string path)
        {
            var mediaList = new List<Media>();

            // get the folders in the path and add to list
            var folders = Directory.GetDirectories(path);
            mediaList.AddRange(GetDirectoryList(folders));

            // get the files in the path and add to list
            var files = Directory.GetFiles(path);
            mediaList.AddRange(GetImageList(files));

            return mediaList;
        }

        private IEnumerable<Media> GetDirectoryList(IEnumerable<string> folders)
        {
            var list = new List<Media>();

            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                var url = Path.Combine(_path, (name ?? ""));

                var media = new Media
                {
                    ContentType = "folder",
                    IsDirectory = true,
                    Name = name,
                    Size = null,
                    Url = url
                };

                list.Add(media);
            }

            return list;
        }

        private IEnumerable<Media> GetImageList(IEnumerable<string> files)
        {
            var list = new List<Media>();

            foreach (var file in files)
            {
                if (!IsImage(file)) continue;

                var name = Path.GetFileName(file);
                var url = Path.Combine(_path, (name ?? ""));

                url = url.Replace(@"\", @"/");

                var item = new Media
                {
                    ContentType = "image",
                    IsDirectory = false,
                    Name = name,
                    Size = 50,
                    Url = url
                };

                list.Add(item);
            }

            return list;
        }

        private bool IsImage(string file)
        {
            var ext = Path.GetExtension(file);

            switch (ext)
            {
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".bmp":
                    return true;
                default:
                    return false;
            }
        }
    }

    public interface IMediaService
    {
        string ResolveMediaPath(string path);
        Transport GetImageTransport(string path);
        byte[] GetFile(string path);
        string GetMimeType(string path);
        bool CreateFolder(string path, string foldername);
        bool StoreFile(HttpPostedFileBase thefile, string path);
    }
}
