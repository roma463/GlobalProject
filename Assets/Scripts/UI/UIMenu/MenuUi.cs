using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private WindowUI _windowSettings;
    [SerializeField] private WindowUI _windowBasic;

    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    private WindowUI _lastOpenWindow;
    private SaveGame _save;

    private void Start ()
    {
        _save = SaveGame.Instance;
        _lastOpenWindow = _windowBasic;
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

    public void SettingsOpen()
    {
        //_animationMenu.SetTrigger("Open");
        ActiveWindow(_windowSettings);
    }

    public void SettingsClosed()
    {
        //_animationMenu.SetTrigger("Closed");
        ActiveWindow(_windowBasic);
    }
    private void ActiveWindow(WindowUI window)
    {
        _lastOpenWindow.Deactivate();
        window.Activate();
        _lastOpenWindow = window;
    }

}
