using TMPro;
using UnityEngine;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [TextArea]
    [SerializeField] private string _russianText, _englishText;

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
        if (SaveGame.Instance == null)
            return;

        if (SaveGame.Instance.Saves.CurrentLanguage == SaveGame.Language.rus)
        {
            _text.text = _russianText;
        }
        else
        {
            _text.text = _englishText;
        }
    }
}
