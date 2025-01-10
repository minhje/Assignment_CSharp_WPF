using Business.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string fileName)
    {
        _filePath = fileName;
    }

    public string GetContentFromFile()
    {
        if (File.Exists(_filePath))
        {
            return File.ReadAllText(_filePath);
        }

        return string.Empty;
    }

    public bool SaveContentToFile(string content)
    {
        try
        {
            File.WriteAllText(_filePath, content);
            return true;
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}