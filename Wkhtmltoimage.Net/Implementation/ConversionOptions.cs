using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Wkhtmltoimage.Net.Implementation.Interfaces;
using Wkhtmltoimage.Net.Options;
using Size = System.Drawing.Size;

namespace Wkhtmltoimage.Net.Implementation
{
    public class ConvertOptions : IConvertOptions
    {
        /// <summary>
        /// Sets image quality.
        /// When jpeg compressing images use this quality (default 94)
        /// </summary>
        [OptionFlag("--quality")]
        public int? ImageQuality { get; set; }

        /// <summary>
        /// Sets height for cropping.
        /// </summary>
        [OptionFlag("--crop-h")]
        public int? CropHeight { get; set; }
        
        /// <summary>
        /// Sets width for cropping
        /// </summary>
        [OptionFlag("--crop-w")]
        public int? CropWidth { get; set; }
        
        /// <summary>
        /// Sets x coordinate for cropping
        /// </summary>
        [OptionFlag("--crop-x")]
        public int? CropX { get; set; }
        
        /// <summary>
        /// Sets y coordinate for cropping
        /// </summary>
        [OptionFlag("--crop-y")]
        public int? CropY { get; set; }
        
        /// <summary>
        /// Sets screen height (default is calculated from page content) (default 0)
        /// </summary>
        [OptionFlag("--height")]
        public int? Height { get; set; }
        
        /// <summary>
        /// Set screen width, note that this is used only as a guide line. 
        /// Use --disable-smart-width to make it strict. (default 1024)
        /// </summary>
        [OptionFlag("--width")]
        public int? Width { get; set; }
        
        /// <summary>
        /// Use --disable-smart-width to make it strict
        /// </summary>
        [OptionFlag("--disable-smart-width")]
        public string? DisableSmartWidth { get; set; }

        /// <summary>
        /// Sets Output file format.
        /// </summary>
        [OptionFlag("--format")]
        public ImageFormat? ImageFormat { get; set; }
        
        

        public string GetConvertOptions()
        {
            var result = new StringBuilder();

            result.Append(" ");
            result.Append(GetConvertBaseOptions());

            return result.ToString().Trim();
        }

        protected string GetConvertBaseOptions()
        {
            var result = new StringBuilder();

            var fields = this.GetType().GetProperties();
            foreach (var fi in fields)
            {
                var of = fi.GetCustomAttributes(typeof(OptionFlag), true).FirstOrDefault() as OptionFlag;
                if (of == null)
                    continue;

                object value = fi.GetValue(this, null);
                if (value == null)
                    continue;

                if (fi.PropertyType == typeof(Dictionary<string, string>))
                {
                    var dictionary = (Dictionary<string, string>)value;
                    foreach (var d in dictionary)
                    {
                        result.AppendFormat(" {0} \"{1}\" \"{2}\"", of.Name, d.Key, d.Value);
                    }
                }
                else if (fi.PropertyType == typeof(bool))
                {
                    if ((bool)value)
                        result.AppendFormat(CultureInfo.InvariantCulture, " {0}", of.Name);
                }
                else
                {
                    result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
                }
            }

            return result.ToString().Trim();
        }
    }
}