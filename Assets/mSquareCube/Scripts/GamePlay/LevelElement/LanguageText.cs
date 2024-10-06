using TMPro;
using UnityEngine;
using Zenject;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [TextArea]
    [SerializeField] private string _russianText, _englishText;
    private SaveGame _save;

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    private void OnValidate()
    {
        if (_text == null)
            TryGetComponent(out _text);
        _text.text = _russianText;
    }

    public virtual void Start()
    {
        UpdateText();
    }

    public void Init(string russian, string english)
    {
        _russianText = russian;
        _englishText = english;
        UpdateText();
    }

    public virtual void UpdateText()
    {
        if (_save == null)
            return;
        Debug.Log(gameObject.name);
        var language = Language.rus;
        if (_save.Saves.CurrentLanguage == language)
        {
            _text.text = _russianText;
        }
        else
        {
            _text.text = _englishText;
        }
    }
}
