namespace DataGrabber
{
    public interface IAccessFileService
    {
        void CreateFile(string fileName);

        void WriteNewLineToFile(string fileName, string text);

        void WriteToFile(string fileName, string text);

        string ReadFile(string fileName);

        string[] GetAllDataFiles();
    }
}