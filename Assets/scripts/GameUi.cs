using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameObject _winDisplay;
    [SerializeField] private GameObject _pauseDisplay;
    [SerializeField] private GameObject _lostDisplay;
    [SerializeField] private Text _nextLevel;
    [SerializeField] private Text _countShotText;
    [SerializeField] private InputButton _inputButton;
    [SerializeField] private Animator _animator;
    public static GameUi GlobalUI { private set; get; }
    private int _currentScene;
    public virtual void Awake()
    {
        GlobalUI = this;
    }
    public virtual void Start()
    {
        _winDisplay.SetActive(true);
        _animator.SetTrigger("StartGame");
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if (_inputButton.Escape)
        {
            Pause();
        }
        else if (_inputButton.KeyR)
        {
            Restart();
        }
    }
    public virtual void Win()
    {
        _animator.SetTrigger("Finish");
        if (SceneManager.sceneCountInBuildSettings- 1 > _currentScene)
        {
            PlayerPrefs.SetInt("Scene", _currentScene + 1);
            _nextLevel.gameObject.SetActive(true);
        }
        StopReadClick(false);
                
    }
    public void Lost() 
    {
        StopReadClick(false);
        _lostDisplay.SetActive(true);
        _inputButton.Pause(false);
    }
    public void DecreaseCountShot(int countShoot)
    {
        _countShotText.text = countShoot.ToString();
    }
    public void Pause()
    {
        _pauseDisplay.SetActive(!_pauseDisplay.activeInHierarchy);
        StopReadClick(!_pauseDisplay.activeInHierarchy);
    }
    private void StopReadClick(bool state)
    {
        _inputButton.Pause(state);
    }
    public virtual void Restart()
    {
        SceneManager.LoadScene(_currentScene);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonPause()
    {
        Pause();
    }
    public void ButtonNextLevel()
    {
        _animator.SetTrigger("Next");
    }
    public virtual void NextLevel()
    {
        SceneManager.LoadScene(_currentScene + 1);
    }
}
