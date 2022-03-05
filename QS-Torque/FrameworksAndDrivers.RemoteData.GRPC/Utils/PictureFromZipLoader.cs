using System.IO;
using System.IO.Compression;
using Core.Entities;

namespace FrameworksAndDrivers.RemoteData.GRPC.Utils
{
    public class PictureFromZipLoader : IPictureFromZipLoader
    {
        public virtual Picture LoadPictureFromZipBytes(byte[] zip)
        {
            if (zip.Length <= 0)
            {
                return null;
            }
            Picture picture = new Picture();
            using (var zipStream = new MemoryStream(zip))
            {
                using (var zipArchive = new ZipArchive(zipStream))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        if (entry.Length == 0)
                        {
                            continue;
                        }
                        picture.ImageStream = new MemoryStream();
                        entry.Open().CopyTo(picture.ImageStream);
                        picture.ImageStream.Position = 0;
                        break;
                    }
                }
            }
            return picture;
        }
    }
}
