using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private Animator _animationMenu;
    [SerializeField] private ConnectionToServer _connetionToServer;
    private int _lastLevel;
    private SaveGame _save;

    private void Start ()
    {
        _save = SaveGame.Instance;
        _lastLevel = _save.Saves.LevelIndex;
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
        _animationMenu.SetTrigger("Open");
    }

    public void SettingsClosed()
    {
        _animationMenu.SetTrigger("Closed");
    }


}
