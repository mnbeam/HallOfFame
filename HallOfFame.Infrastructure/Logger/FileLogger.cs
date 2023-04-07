using System.Text;

namespace HallOfFame.Infrastructure.Logger;

public class FileLogger
{
    private static readonly object _lock = new();
    private readonly string _rootPath;

    public FileLogger(string rootPath)
    {
        _rootPath = rootPath;
    }

    public void LogError(string errorMessage)
    {
        lock (_lock)
        {
            Directory.CreateDirectory(_rootPath);
            using var stream =
                File.Open(Path.Combine(_rootPath, "logs.txt"), FileMode.OpenOrCreate);
            using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine($"{DateTime.Now}: {errorMessage}");
        }
    }
}