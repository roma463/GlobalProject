using UnityEngine;

public class UIServer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _coopButton;
    [SerializeField] private GameObject _coopWindow;
    [SerializeField] private GameObject _textConnetcionInternet;

    public void ActiveCoopButton()
    {
        _coopButton.interactable = true;
        _textConnetcionInternet.SetActive(false);
    }

    public void ClickToCoop()
    {
        _coopWindow.SetActive(true);
    }
}
