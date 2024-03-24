using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public static GameUi GlobalUI { private set; get; }
    [SerializeField] private GameObject _winDisplay;
    [SerializeField] private GameObject _pauseDisplay;
    [SerializeField] private GameObject _lostDisplay;
    [SerializeField] private InputButton _inputButton;
    [SerializeField] protected Animator _animator;

    [SerializeField] private float _pauseWindowOpen;
    [SerializeField] private float _pauseWindowClosed;
    [SerializeField] private float _speedOpen;
    private Coroutine _pauseCorutine;
    private bool _pauseOpen = false;

    private int _currentScene;

    public virtual void Awake()
    {
        _pauseDisplay.SetActive(true);
        _pauseDisplay.transform.position = Vector3.right * _pauseWindowClosed;
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
        //if (_inputButton.Escape)
        //{
        //    Pause();
        //}
        //else if (_inputButton.KeyR)
        //{
        //    Restart();
        //}
    }
    public virtual void Win()
    {
        if (SceneManager.sceneCountInBuildSettings- 1 > _currentScene)
        {
            _animator.SetTrigger("Finish");
            PlayerPrefs.SetInt("Scene", _currentScene + 1);
        }
        StopReadClick(false);
                
    }
    public void Lost() 
    {
        StopReadClick(false);
        _lostDisplay.SetActive(true);
        _inputButton.Pause(false);
    }
    public void Pause()
    {
        if (_pauseCorutine == null)
        {
            var target = _pauseOpen ? _pauseWindowClosed : _pauseWindowOpen;
            _pauseOpen = !_pauseOpen;
            StopReadClick(!_pauseOpen);
            _pauseCorutine = StartCoroutine(ActivityPause(target));
        }
    }
    private IEnumerator ActivityPause(float target)
    {
        float currentValue = _pauseDisplay.transform.position.x;
        while(currentValue != target)
        {
            yield return null;
            currentValue = Mathf.MoveTowards(currentValue, target, Time.deltaTime * _speedOpen);
            _pauseDisplay.transform.position = Vector3.right * currentValue;
        }
        _pauseCorutine = null;
    }
    public virtual void StopReadClick(bool state)
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
