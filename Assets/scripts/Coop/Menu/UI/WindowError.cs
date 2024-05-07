using TMPro;
using UnityEngine;

public class WindowError : WindowUI
{
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private string _text;

    private void Start()
    {
        _message.text = _text;
    }
}
