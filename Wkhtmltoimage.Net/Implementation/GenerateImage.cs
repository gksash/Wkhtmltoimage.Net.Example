using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wkhtmltoimage.Net.Implementation.Interfaces;

namespace Wkhtmltoimage.Net.Implementation
{
    public class GenerateImage : IGenerateImage
    {
        protected IConvertOptions _convertOptions;
        // readonly IRazorViewToStringRenderer _engine;

        public GenerateImage()
        {
            _convertOptions = new ConvertOptions();
        }

        public void SetConvertOptions(IConvertOptions convertOptions)
        {
            _convertOptions = convertOptions;
        }
        
        public string GetImage(string url, string imageSavePath)
        {
            return WkhtmlDriver.Convert(WkhtmltoimageConfiguration.WkhtmltoimagePath, _convertOptions.GetConvertOptions(), url, imageSavePath);
        }
    }
}