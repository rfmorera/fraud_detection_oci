using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetectionOci
{
    public class RenameFiles
    {
        public static void Rename(string basefolder)
        {
            var folderNames = Directory.EnumerateDirectories(basefolder);

            var day = new DirectoryInfo(basefolder).Name;
            day = day.ToLower();
            day = day.Replace(" ", "");
            foreach (string folder in folderNames)
            {
                var filesNames = Directory.EnumerateFiles(folder);
                var user = new DirectoryInfo(folder).Name;
                foreach (string file in filesNames)
                {
                    string envio = new FileInfo(file).Name;
                    string newFile = $"{folder}\\{day}_{user}_{envio}";
                    File.Move(file, newFile);
                }
            }
        }
    }
}
