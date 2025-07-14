namespace InternshipEntryTask.Infrastructure.Extensions;

public static class ProbabilityExtension
{
    public static bool NextProbability(this Random random, int probability)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(probability, 0, nameof(probability));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, 100, nameof(probability));
        
        return random.Next(100) < probability;
    }
}