namespace HallOfFame.Core.Exceptions;

public class HallOfFameNotFoundException : Exception
{
    public HallOfFameNotFoundException(string name)
        : base($"Entity {name} not found")
    {
    }
}