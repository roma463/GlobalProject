using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(ButtonClosed);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide() 
    {
        gameObject.SetActive(false);
    }

    public void ButtonClosed()
    {
        Hide();
    }
}
