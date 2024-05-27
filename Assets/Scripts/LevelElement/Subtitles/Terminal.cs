using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Terminal : WindowUI
{
    [SerializeField] private List<Massage> _massages = new List<Massage>();
    [SerializeField] private float _delay;
    [SerializeField] private TextMeshProUGUI _outputText;

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(OutputMessage());
    }

    private IEnumerator OutputMessage()
    {
        yield return new WaitForSeconds(_delay);
        foreach (var item in _massages)
        {
            var massage = item.GetMassage();
            for (int i = 0; i < massage.Length; i++)
            {
                _outputText.text += massage[i];
                yield return null;
            }
            _outputText.text += "\n";
            yield return new WaitForSeconds(item.GetDelay());
        }
    }
}

[Serializable]
public class Massage
{
    [TextArea(5,5)]
    [SerializeField] private string _massage;
    [SerializeField] private float _delay = 1;

    public float GetDelay() => _delay;

    public string GetMassage() => _massage;
}
