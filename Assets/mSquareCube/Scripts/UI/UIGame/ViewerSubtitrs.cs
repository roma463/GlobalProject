using TMPro;
using UnityEngine;

public class ViewerSubtitrs : MonoBehaviour
{
    public static ViewerSubtitrs Instance { private set; get; }
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _element;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateWindow()
    {
        _element.SetActive(true);
    }

    public void DeactivateWindow()
    {
        _element.SetActive(false);
    }

    public void ViewText(string text)
    {
        _text.text = text;
    }
}
