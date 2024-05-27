using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviourPunCallbacks
{
    public static GameUi Instance { private set; get; }
    [SerializeField] protected Animator _animator;
    [SerializeField] private UnityEvent e_startGame;
    [SerializeField] private GameObject _winDisplay;
    [SerializeField] private GameObject _pauseDisplay;

    [SerializeField] private float _pauseWindowOpen;
    [SerializeField] private float _pauseWindowClosed;
    [SerializeField] private float _speedOpen;
    private int _currentScene;
    private Coroutine _pauseCorutine;

    public virtual void Awake()
    {
        _pauseDisplay.SetActive(true);
        //_pauseDisplay.transform.localPosition =  Vector3.right * _pauseWindowClosed;
        Instance = this;
    }

    public virtual void Start()
    {
        _winDisplay.SetActive(true);
        _animator.SetTrigger("StartGame");
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public int GetIndexCurrentScene()
    {
        return _currentScene;
    }
    
    public virtual void WinWindow()
    {
        if (SceneManager.sceneCountInBuildSettings- 1 > _currentScene)
        {
            _animator.SetTrigger("Finish");
        }
    }

    public void EndStartAnimation()
    {
        e_startGame?.Invoke();
    }

    public void PauseWindow(bool state)
    {
        if (_pauseCorutine == null)
        {
            var target = state ? _pauseWindowClosed : _pauseWindowOpen;
            _pauseCorutine = StartCoroutine(PauseAnimation(target));
        }
    }

    private IEnumerator PauseAnimation(float target)
    {
        float currentValue = _pauseDisplay.transform.localPosition.x;
        while(currentValue != target)
        {
            yield return null;
            currentValue = Mathf.MoveTowards(currentValue, target, Time.deltaTime * _speedOpen);
            _pauseDisplay.transform.localPosition = Vector3.right * currentValue;
        }
        _pauseCorutine = null;
    }

    public virtual void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Вызывается при проигрывании анимации пробеды
    public virtual void NextLevel()
    {
        SceneManager.LoadScene(_currentScene + 1);
    }
}
