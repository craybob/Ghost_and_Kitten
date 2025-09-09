public class LevelModel
{
    public int CurrentLevel { get; private set; } = 1;
    public float TrackLength { get; private set; } = 40f;
    public int ObstacleCount { get; private set; } = 3;

    public event System.Action<int, float, int> OnLevelGenerated;

    public void GenerateNextLevel()
    {
        TrackLength += 5f; // усложнение Ч длина дорожки больше
        ObstacleCount += 2; // добавл€ем больше преп€тствий
        CurrentLevel++;

        OnLevelGenerated?.Invoke(CurrentLevel, TrackLength, ObstacleCount);
    }
}
