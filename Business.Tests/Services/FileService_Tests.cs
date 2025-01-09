using Business.Interfaces;
using Business.Services;

namespace Business.Tests.Services;


// Detta test är från videon Tips & Trix - Skapa en fungerande applikation med tester(https://www.youtube.com/watch?v=mdzDKUO3jKQ&t=1s)
public class FileService_Tests
{

    [Fact]
    public void SaveContentToFile_ShouldCreateFileWithContent()
    {
        // Arrange
        var content = "test_content";
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        IFileService fileService = new FileService(fileName);


        try
        {
            // Act
            var result = fileService.SaveContentToFile(content);

            // Assert
            Assert.True(result);

        }
        finally
        {
            if (File.Exists(fileName))
            {
                // Cleanup
                File.Delete(fileName);
            }
        }
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnContentFromFile()
    {
        // Arrange
        var content = "test_content";
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        File.WriteAllText(fileName, content);

        IFileService fileService = new FileService(fileName);
        fileService.SaveContentToFile(content);

        try
        {
            // Act
            var result = fileService.GetContentFromFile();

            // Assert
            Assert.Equal(content, result);
        }
        finally
        {
            if (File.Exists(fileName))
            {
                // Cleanup
                File.Delete(fileName);
            }
        }
    }

}