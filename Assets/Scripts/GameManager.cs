using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CatView catView;
    [SerializeField] private PlayerView playerView;   // твой призрак
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SafeZone safeZone;

    private CatPresenter _catPresenter;
    private PlayerPresenter _playerPresenter;

    private LevelPresenter _levelPresenter;
    private LevelModel _levelModel;
    [SerializeField] private LevelView _levelView;

    private void Start()
    {
        //Level Generate

        _levelModel = new LevelModel();
        _levelPresenter = new LevelPresenter(_levelModel, _levelView);

        //for safeZone in Lvl generate
        _levelView.gameManager = this;

        _levelPresenter.StartGame();

        // Cat
        var catModel = new CatModel();
        _catPresenter = new CatPresenter(catView, catModel);
        _catPresenter.OnHealthChanged += uiManager.SetHealthSlider;
        _catPresenter.OnCatUpset += LoseGame;              // событие поражения
        catView.SetPresenter(_catPresenter);
        uiManager.SetHealthSlider(catModel.Mood);

        // Player (ghost)
        var playerModel = new PlayerModel();
        _playerPresenter = new PlayerPresenter(playerView, playerModel);
        playerView.SetPresenter(_playerPresenter);

        // Победа по входу кота в Safe зону (без поллинга)
        safeZone.OnCatEntered += () =>
        {
            if (_catPresenter.Mood > 0f) WinGame();
        };

        

        
    }

    private void Update()
    {
        _catPresenter.OnUpdate();
        _playerPresenter.OnUpdate();
    }

    public void GetSafeZone(SafeZone safeZone)
    {
        this.safeZone = safeZone;
    }

    private void WinGame() { Debug.Log("Победа!"); _levelPresenter.NextLevel(); }
    private void LoseGame() { Debug.Log("Поражение!"); }
}
