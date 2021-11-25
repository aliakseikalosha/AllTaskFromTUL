using CleanPRJ.Droid;
using System.IO;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(AccessFileImplement))]
namespace CleanPRJ.Droid
{
    public class AccessFileImplement : DataProvider.IAccessFileService
    {
        private static string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "DataGrabber");

        public string[] GetAllDataFiles()
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path, "*.csv").Where(path=> new FileInfo(path).Length > 10).OrderBy(c=>c).ToArray();
            }
            return null;
        }

        public string ReadFile(string fileName)
        {
            var fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
            return null;
        }

        public void WriteNewLineToFile(string fileName, string text)
        {
            var fullPath = Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                ((DataProvider.IAccessFileService)this).CreateFile(fileName);
            }
            File.AppendAllText(fullPath, text + "\n");
        }

        public void WriteToFile(string fileName, string text)
        {
            var fullPath = Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                ((DataProvider.IAccessFileService)this).CreateFile(fileName);
            }
            File.WriteAllText(fullPath, text + "\n");
        }

        void DataProvider.IAccessFileService.CreateFile(string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = Path.Combine(path, fileName);
            File.WriteAllText(filePath, "");
        }
    }
}