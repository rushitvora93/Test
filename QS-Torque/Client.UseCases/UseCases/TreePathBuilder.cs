using System;
using System.Linq;
using System.Text;
using Core.Entities;

namespace Core.UseCases
{
    public interface ITreePathBuilder
    {
        string GetTreePath(Location location, string seperator);
        string GetMaskedTreePathWithBase64(Location location);
        string GetDeMaskedTreePathFromBase64(string path, string seperator);
    }

    public class TreePathBuilder : ITreePathBuilder
    {
        public string GetTreePath(Location location, string seperator)
        {
            if (location == null)
            {
                throw new ArgumentException("GetTreePath for Location with null!");
            }

            if (location.LocationDirectoryPath == null)
            {
                return "";
            }

            var path = "";
            for (var i = 0; i < location.LocationDirectoryPath.Count; i++)
            {
                path += location.LocationDirectoryPath[i].Name.ToDefaultString();
                if (i != location.LocationDirectoryPath.Count - 1)
                {
                    path += seperator;
                }
            }

            return path;
        }

     

        public string GetMaskedTreePathWithBase64(Location location)
        {
            if (location == null)
            {
                throw new ArgumentException("GetTreePath for Location with null!");
            }

            if (location.LocationDirectoryPath == null)
            {
                return "";
            }

            var path = "";
           
            for (var i = 0; i < location.LocationDirectoryPath.Count; i++)
            {
                var directoryName = location.LocationDirectoryPath[i].Name.ToDefaultString();
                path += Convert.ToBase64String(Encoding.UTF8.GetBytes(directoryName));

                if (i != location.LocationDirectoryPath.Count - 1)
                {
                    path += "|";
                }
            }

            return path;
        }

        public string GetDeMaskedTreePathFromBase64(string path, string seperator)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            var demaskedPath = "";
            var splittedPath = path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var i = 0; i < splittedPath.Count; i++)
            {
                demaskedPath += Encoding.UTF8.GetString(Convert.FromBase64String(splittedPath[i]));
                if (i != splittedPath.Count - 1)
                {
                    demaskedPath += seperator;
                }
            }

            return demaskedPath;
        }
    }
}
