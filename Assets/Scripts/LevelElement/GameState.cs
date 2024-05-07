using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private LevelList _levelList;
    public static GameState Instance { get; private set; }
    protected int CurrentScene;
    private GameUi _gameUi;
    private InputButton _playerButton;
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
        _playerButton = inputButton;
    }

    public virtual void Update()
    {
        if (_playerButton.Escape)
        {
            _gameUi.PauseWindow(_isGamePause);
            PauseGame();
        }
        else if (_playerButton.KeyR)
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
        _playerButton.Pause(_isGamePause);
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
