using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Wkhtmltoimage.Net
{
    public abstract class WkhtmlDriver
    {
        /// <summary>
        /// Converts given URL or HTML string to PDF.
        /// </summary>
        /// <param name="wkhtmlPath">Path to wkhtmltoimage.</param>
        /// <param name="arguments">Arguments that will be passed to wkhtmltoimage binary.</param>
        /// <param name="url">String containing Url of page that should be converted to Image.</param>
        /// <param name="destinationFileNamePath">String containing path to save image.</param>
        /// <returns>Console output</returns>
        public static string Convert(string wkhtmlPath, string arguments, string url, string destinationFileNamePath = "")
        {
            string wkHtmlToImageLocation;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                wkHtmlToImageLocation = Path.Combine(wkhtmlPath, "Windows", "wkhtmltoimage.exe");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                wkHtmlToImageLocation = Path.Combine(wkhtmlPath, "Mac", "wkhtmltoimage");
            }
            else
            {
                wkHtmlToImageLocation = Path.Combine(wkhtmlPath, "Linux", "wkhtmltoimage");
            }

            if (!File.Exists(wkHtmlToImageLocation))
            {
                throw new Exception("wkhtmltoimage not found, searched for " + wkHtmlToImageLocation);
            }
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                arguments = $"{arguments} {url} {destinationFileNamePath}";
                
                using (var proc = new Process())
                {
                    proc.StartInfo = new ProcessStartInfo
                    {
                        FileName = wkHtmlToImageLocation,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        // RedirectStandardInput = true,
                        CreateNoWindow = true
                    };

                    proc.Start();

                    using (var ms = new MemoryStream())
                    {
                        using (var sOut = proc.StandardOutput.BaseStream)
                        {
                            byte[] buffer = new byte[4096];
                            int read;

                            while ((read = sOut.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                        }

                        if (!proc.WaitForExit(60000))
                        {
                            proc.Kill();
                        }
                        
                        return proc.StandardError.ReadToEnd(); 
                    }
                }
            }
            else
            {
                arguments = $"{arguments} {url} {destinationFileNamePath}";

                using (var proc = new Process())
                {
                    proc.StartInfo = new ProcessStartInfo
                    {
                        FileName = wkHtmlToImageLocation,
                        Arguments = arguments,
                        UseShellExecute = false,
                        //RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        //RedirectStandardInput = true,
                        CreateNoWindow = true
                    };

                    proc.Start();

                    using (var ms = new MemoryStream())
                    {
                        string consoleOutput = proc.StandardError.ReadToEnd();

                        if (!proc.WaitForExit(60000))
                        {
                            proc.Kill();
                        }

                        return consoleOutput;
                    }
                }
            }
        }
    }
}