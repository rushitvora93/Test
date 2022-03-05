using Core.Entities;

public interface IPictureFromZipLoader
{
    Picture LoadPictureFromZipBytes(byte[] zip);
}