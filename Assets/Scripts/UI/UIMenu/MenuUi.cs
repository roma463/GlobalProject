using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    public static MenuUi Instance { get; private set; }
    [SerializeField] private WindowUI w_basic;

    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    private List<WindowUI> _listOpenWindow = new List<WindowUI>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
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
    }

    public void ClosedWindow()
    {
        var lastOpenWindow = _listOpenWindow[_listOpenWindow.Count-1];
        _listOpenWindow.Remove(lastOpenWindow);
        ActiveWindow(_listOpenWindow[_listOpenWindow.Count - 1], lastOpenWindow);
    }

    private void ActiveWindow(WindowUI windowOpen, WindowUI windowClosed)
    {
        windowOpen.Activate();
        windowClosed.Deactivate();
    }

}
