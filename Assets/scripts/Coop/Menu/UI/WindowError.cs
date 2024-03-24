using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowError : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _message;
    public void Active(string error)
    {
        gameObject.SetActive(true);
        _message.text = error;
    }

    public void CliskClosedWindow()
    {
        gameObject.SetActive(false);
    }
}
