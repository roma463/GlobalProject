using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    public static MenuUi Instance { get; private set; }
    [SerializeField] private WindowUI w_basic;

    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    private WindowUI _lastOpenWindow;
    private SaveGame _save;

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        _save = SaveGame.Instance;
        _lastOpenWindow = w_basic;
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
        //_animationMenu.SetTrigger("Open");
        ActiveWindow(window);
    }

    public void ClosedWindow()
    {
        //_animationMenu.SetTrigger("Closed");
        ActiveWindow(w_basic);
    }

    private void ActiveWindow(WindowUI window)
    {
        _lastOpenWindow.Deactivate();
        window.Activate();
        _lastOpenWindow = window;
    }

}
