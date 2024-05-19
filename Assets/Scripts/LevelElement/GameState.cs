using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private LevelList _levelList;
    public static GameState Instance { get; private set; }
    protected int CurrentScene;
    protected InputButton PlayerButton;
    private GameUi _gameUi;
    private bool _isGamePause = false;
    private SaveGame _saveGame;


    private void Awake()
    {
        Instance = this;
    }

    public virtual void Start()
    {
        _saveGame = SaveGame.Instance;
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
        var nextLevel = _levelList.GetSingleNextLevel(_saveGame.Saves.LevelSingleIndex);
        SceneManager.LoadScene(nextLevel);
    }

    public void Win()
    {
        _saveGame.Saves.LevelSingleIndex++;
        _saveGame.SaveData();
        
        _gameUi.WinWindow();
        PauseGame();
        //PlayerPrefs.SetInt("Scene", CurrentScene + 1);
    }

}
