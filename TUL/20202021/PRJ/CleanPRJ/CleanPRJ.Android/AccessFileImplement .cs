using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CleanPRJ.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(AccessFileImplement))]
namespace CleanPRJ.Droid
{
    public class AccessFileImplement : DataProvider.IAccessFileService
    {
        private static string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "DataGrabber");
        public void WriteNewLineToFile(string fileName, string text)
        {
            var fullPath = Path.Combine(path, fileName);
            if (!File.Exists(fullPath))
            {
                ((DataProvider.IAccessFileService)this).CreateFile(fileName);
            }
            File.AppendAllText(fullPath, text + "\n");
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