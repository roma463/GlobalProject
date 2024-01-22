using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private Animator _animationMenu;
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
        SceneManager.LoadScene(_lastLevel);
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
