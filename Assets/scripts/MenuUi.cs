using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    private int _lastLevel;
    private void Start()
    {
        _lastLevel = PlayerPrefs.GetInt("Scene", 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene(_lastLevel);
    }
}
