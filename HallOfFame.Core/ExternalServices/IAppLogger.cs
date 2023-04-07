namespace HallOfFame.Core.ExternalServices;

public interface IAppLogger<T>
{
    void LogError(string messageError);
}