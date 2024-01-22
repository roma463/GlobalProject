using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMenu : LanguageText
{
    public override void Start()
    {
        MenuSettings.Instance.LanguageUpdate += UpdateText;
        base.Start();
    }

    public override void UpdateText()
    {
        base.UpdateText();
    }
}
