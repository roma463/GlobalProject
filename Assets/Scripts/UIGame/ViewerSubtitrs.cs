using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewerSubtitrs : MonoBehaviour
{
    public static ViewerSubtitrs Instance { private set; get; }
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        Instance = this;
    }

    public void ViewText(string text)
    {
        _text.text = text;
    }
}
