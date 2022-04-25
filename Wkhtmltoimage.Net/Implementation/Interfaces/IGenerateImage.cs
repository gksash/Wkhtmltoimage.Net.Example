namespace Wkhtmltoimage.Net.Implementation.Interfaces
{
    public interface IGenerateImage
    {
        string GetImage(string url, string imageSavePath);
        void SetConvertOptions(IConvertOptions options);
    }
}