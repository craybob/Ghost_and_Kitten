public class LevelPresenter
{
    private readonly LevelModel _model;
    private readonly LevelView _view;

    public LevelPresenter(LevelModel model, LevelView view)
    {
        _model = model;
        _view = view;

        _model.OnLevelGenerated += HandleLevelGenerated;
    }

    public void StartGame()
    {
        _model.GenerateNextLevel();
    }

    public void NextLevel()
    {
        _model.GenerateNextLevel();
    }

    private void HandleLevelGenerated(int level, float length, int obstacles)
    {
        _view.RenderLevel(length, obstacles);
    }
}
