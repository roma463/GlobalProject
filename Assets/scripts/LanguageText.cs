using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private TextMesh _mesh;
    [TextArea]
    [SerializeField] private string _russianText, _englishText;
    private void Start()
    {
        if (PlayerPrefs.GetString("Language", "rus") == "rus")
        {
            _mesh.text = _russianText;
        }
        else
        {
            _mesh.text = _englishText;
        }
    }
}
