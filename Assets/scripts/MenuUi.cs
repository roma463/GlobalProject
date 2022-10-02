using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    private int _lastLevel;
    [SerializeField] private Animator _animSquare;
    [SerializeField] private Animator _animCamera;
    [SerializeField] private Animator _animButton;
    
    private void Start ()
    {
        _lastLevel = 1;
    }

    public void FirstStart ()
    {
        _animSquare.SetTrigger("_isDisperse");
        _animCamera.SetTrigger("_isMoveCamera");
        _animButton.SetTrigger("_isMoveButton");
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
