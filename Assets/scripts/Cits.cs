using UnityEngine;

public class Cits : MonoBehaviour
{
    [SerializeField] private GameUi _ui;
    [SerializeField] private InputButton _inputButton;
    private bool _isStop;

    private void Update()
    {
        if(_inputButton.Word.IndexOf("DON") == 0 && !_isStop)
        {
            _ui.Win();
            _isStop = true;
        }

            print(_inputButton.Word.IndexOf("DON"));
    }
}
