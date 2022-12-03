using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BroadcastTool
{
    internal class Util
    {
        public static IEnumerable<string> getFilePaths(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return Directory.EnumerateFiles(directoryPath);
        }

        public static BitmapImage getBitmapImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();
            return image;
        }
    }
}
