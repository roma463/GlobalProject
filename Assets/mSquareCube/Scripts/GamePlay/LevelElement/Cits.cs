using UnityEngine;

public class Cits : MonoBehaviour
{
    [SerializeField] private InputButton _inputButton;
    private GameUi _ui;

    private void Start()
    {
        _ui = GameUi.Instance;
    }
    
    private void Update()
    {
         if (Input.anyKey)
         {

             if(_inputButton.Word.Contains("DON"))
             {
                GameState.Instance.Win();
                _inputButton.ResetListKey();
             }
            if (_inputButton.Word.Contains("MEME"))
            {
                _ui.WinWindow();
                _inputButton.ResetListKey();
            }
        }
    }
}
