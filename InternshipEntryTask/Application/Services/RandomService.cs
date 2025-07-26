using InternshipEntryTask.Application.Interfaces;

namespace InternshipEntryTask.Application.Services;

public class RandomService : IRandomService
{
    private readonly Random _random = new Random();
    
    public bool NextProbability(byte probability)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, 100, nameof(probability));
        
        return _random.Next(100) < probability;
    }
}