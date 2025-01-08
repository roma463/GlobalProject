using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class ViewerSubtitrs : MonoBehaviour
{
    public static ViewerSubtitrs Instance { private set; get; }
    public bool IsVieweble { get; private set; }

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
        //_text.text = text;
        StartCoroutine(TextAnimation(text));
    }

    private IEnumerator TextAnimation(string newText)
    {
        _text.text = newText;
        IsVieweble = true;


        for (int i = 0; i < newText.Length; i++)
        {
            _text.maxVisibleCharacters = i;
            AudioController.ViewText();
            yield return new WaitForSecondsRealtime(.05f);
        }
        _text.maxVisibleCharacters = newText.Length;
        IsVieweble = false;
    }
}
