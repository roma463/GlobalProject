using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public bool IsPaused { get; private set; }

    protected SaveGame _save;
    protected int CurrentScene;
    protected InputButton InputButtons;
    private GameUi _gameUi;

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
        InputButtons = InputButton.Instance;
    }

    public virtual void Update()
    {
        if (InputButtons.Escape)
        {
            _gameUi.PauseWindow(IsPaused);
            PauseGame();
        }
        else if (InputButtons.KeyR)
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
        IsPaused = !IsPaused;
        InputButtons.Pause(IsPaused);
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
    }
    public virtual void SaveLevel()
    {
        _save.Data.LevelSingleIndex++;
        _save.SaveData();
    }

}
