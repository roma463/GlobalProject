using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] private Transform _pointCurrentLenguage;
    
    [SerializeField] private Transform _pointEnglish, _pointRussian;
    private int _lastLevel;
  
    private void Start ()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey("Scene");
#endif
        _lastLevel = PlayerPrefs.GetInt("Scene", 1);
        if(PlayerPrefs.GetString("Language", "rus") == "rus")
        {
            _pointCurrentLenguage.position = _pointRussian.position;
        }
        else
        {
            _pointCurrentLenguage.position = _pointEnglish.position;
        }
    }
    public void Language(string name)
    {
        PlayerPrefs.SetString("Language", name);
        if (name == "rus")
        {
            _pointCurrentLenguage.position = _pointRussian.position;
        }
        else
        {
            _pointCurrentLenguage.position = _pointEnglish.position;
        }
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
