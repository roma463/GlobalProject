using UnityEngine;

public class Cits : MonoBehaviour
{
    [SerializeField] private InputButton _inputButton;
    private GameUi _ui;
    private void Start()
    {
        _ui = GameUi.GlobalUI;
    }
    private void Update()
    {
         if (Input.anyKey)
         {

             if(_inputButton.Word.Contains("DON"))
             {
                 _ui.Win();
                _inputButton.ResetListKey();
             }
            if (_inputButton.Word.Contains("ALAX"))
            {
                _ui.Win();
                _inputButton.ResetListKey();
            }
        }
    }
}
