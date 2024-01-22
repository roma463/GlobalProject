using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mesh;
    [TextArea]
    [SerializeField] private string _russianText, _englishText;
    public virtual void Start()
    {
        UpdateText();
    }
    public virtual void UpdateText()
    {
        if (SaveGame.Instance.Saves.CurrentLanguage == SaveGame.Language.rus)
        {
            _mesh.text = _russianText;
        }
        else
        {
            _mesh.text = _englishText;
        }
    }
}
