using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameState : MonoBehaviour
{
    [SerializeField] protected LevelList _levelList;
    public static GameState Instance { get; private set; }
    protected SaveGame _save;
    protected int CurrentScene;
    protected InputButton PlayerButton;
    private GameUi _gameUi;
    private bool _isGamePause = false;

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    private void Awake()
    {
        Instance = this;
    }

    public virtual void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        _gameUi = GameUi.Instance;
    }

    public void Initialize(InputButton inputButton)
    {
        PlayerButton = inputButton;
    }

    public virtual void Update()
    {
        if (PlayerButton.Escape)
        {
            _gameUi.PauseWindow(_isGamePause);
            PauseGame();
        }
        else if (PlayerButton.KeyR)
        {
            Restart();
        }
    }

    public virtual void Restart()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void PauseGame()
    {
        _isGamePause = !_isGamePause;
        PlayerButton.Pause(_isGamePause);
    }

    public virtual void LoadNextLevel()
    {
        var nextLevel = _save.Data.LevelSingleIndex;
        SceneManager.LoadScene(nextLevel);
    }

    public void Win()
    {
        SaveLevel();
        _gameUi.WinWindow();
        PauseGame();
    }
    public virtual void SaveLevel()
    {
        _save.Data.LevelSingleIndex++;
        _save.SaveData();
    }

}
