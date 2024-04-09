using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private int _currentScene;
    private GameUi _gameUi;
    private InputButton _playerButton;
    private bool _isGamePause = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerButton = InputButton.Instance;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _gameUi = GameUi.Instance;
    }

    public void Initialize(InputButton inputButton)
    {
        _playerButton = inputButton;
    }

    private void Update()
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

    private void Restart()
    {
        SceneManager.LoadScene(_currentScene);
    }

    public void PauseGame()
    {
        _isGamePause = !_isGamePause;
        _playerButton.Pause(_isGamePause);
    }

    public void Win()
    {
        _gameUi.WinWindow();
        PlayerPrefs.SetInt("Scene", _currentScene + 1);
    }

}
