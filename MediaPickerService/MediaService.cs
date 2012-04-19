using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using MediaPickerService.Dto;

namespace MediaPickerService
{
    public class MediaService : IMediaService
    {
        public string ResolveMediaPath(string path)
        {
            var root = ConfigurationManager.AppSettings["MediaDirectory"];
            if (path == null)
                path = "";

            // if it starts with a squiggly we need to resolve by the application root
            if (string.IsNullOrEmpty(root))
                return "";

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
            var breadcrumbs = BuildBreadcrumbs(physical);

            // return transport object.
            return new Transport
            {
                Breadcrumbs = breadcrumbs,
                Mediae = mediae
            };
        }

        private IEnumerable<Breadcrumb> BuildBreadcrumbs(string path)
        {
            return new List<Breadcrumb>();
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

        private IEnumerable<Media> GetImageList(IEnumerable<string> files)
        {
            var list = new List<Media>();

            foreach (var file in files)
            {
                if (!IsImage(file)) continue;

                var item = new Media
                {
                    Icon = "image.png",
                    IsDirectory = false,
                    Name = Path.GetFileName(file),
                    Size = 50,
                    Url = "???"
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

        private IEnumerable<Media> GetDirectoryList(IEnumerable<string> folders)
        {
            var list = new List<Media>();

            foreach (var folder in folders)
            {
                var media = new Media
                {
                    Icon = "folder.png",
                    IsDirectory = true,
                    Name = Path.GetFileName(folder),
                    Size = null,
                    Url = "???"
                };

                list.Add(media);
            }

            return list;
        }
    }

    public interface IMediaService
    {
        string ResolveMediaPath(string path);
        Transport GetImageTransport(string path);
    }
}
