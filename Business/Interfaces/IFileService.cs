using Business.Models;

namespace Business.Interfaces
{
    public interface IFileService
    {
        bool SaveContentToFile(string content);
        string GetContentFromFile();
    }
}