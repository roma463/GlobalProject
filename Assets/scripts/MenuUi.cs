using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private Text _lastLevelText;
    private int _lastLevel;
    private void Start()
    {
        _lastLevel = PlayerPrefs.GetInt("Scene", 1);
        _lastLevelText.text = _lastLevel.ToString();
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
