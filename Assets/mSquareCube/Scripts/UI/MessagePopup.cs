using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopup : PopupParameters
{
    public string message;
    public Action continueButton;
    public Action cancelButton;
}
