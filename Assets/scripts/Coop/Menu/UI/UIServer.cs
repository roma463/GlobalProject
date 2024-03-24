using UnityEngine;

public class UIServer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _coopButton;
    [SerializeField] private GameObject _coopWindow;

    public void ActiveCoopButton()
    {
        _coopButton.interactable = true;
    }

    public void ClickToCoop()
    {
        _coopWindow.SetActive(true);
    }
}
