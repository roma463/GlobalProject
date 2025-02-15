using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuUi : MonoBehaviour
{
    public static MenuUi Instance { get; private set; }
    [SerializeField] private WindowUI w_basic;

    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _back;
    private List<WindowUI> _listOpenWindow = new List<WindowUI>();
    private SaveGame _saveGame;

    [Inject]
    public void Construct(SaveGame save)
    {
        _saveGame = save;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        if (_saveGame.Data.LevelSingleIndex != SaveGame.startLevelIndex)
            _continue.interactable = true;
        _listOpenWindow.Add(w_basic);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        _connetionToServer.StartCoroutine(_connetionToServer.Disconection(LoadScene));
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenWindow(WindowUI window)
    {
        _listOpenWindow.Add(window);
        ActiveWindow(window, _listOpenWindow[_listOpenWindow.Count - 2]);
        StateBackButton();

    }

    public void ClosedWindow()
    {
        var lastOpenWindow = _listOpenWindow[_listOpenWindow.Count - 1];
        _listOpenWindow.Remove(lastOpenWindow);
        ActiveWindow(_listOpenWindow[_listOpenWindow.Count - 1], lastOpenWindow);
        StateBackButton();
    }

    private void StateBackButton()
    {
        if(_listOpenWindow.Count > 1)
            _back.gameObject.SetActive(true);
        else
            _back.gameObject.SetActive(false);
    }

    private void ActiveWindow(WindowUI windowOpen, WindowUI windowClosed)
    {
        windowOpen.Show();
        windowClosed.Hide();
    }

}
