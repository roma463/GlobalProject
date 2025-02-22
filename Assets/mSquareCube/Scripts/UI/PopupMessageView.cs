using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessageView : PopupView<MessagePopup>
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _cancel;

    public override void Setup(MessagePopup settings)
    {
        _title.text = settings.message;

        InitButton(_continue, settings.continueButton);
        InitButton(_cancel, settings.cancelButton);

        Show();
    }

    private void InitButton(Button button, Action? action)
    {
        if(action == null)
        {
            button.gameObject.SetActive(false);
            return;
        }

        button.onClick.AddListener(() => action?.Invoke());
    }
}
