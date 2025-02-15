using UnityEngine;

public class Cits : MonoBehaviour
{
    private InputButton _inputButton;
    private GameUi _ui;

    private void Start()
    {
        DontDestroyOnLoad(this);
        _inputButton = InputButton.Instance;
        _ui = GameUi.Instance;
    }
    
    private void Update()
    {
         if (Input.anyKey)
         {
            if (_inputButton.Word.Contains("MUSIC"))
            {
                MusicWindow.Instance?.Show();
                Equlizer.Activate();
                _inputButton.ResetListKey();
            }

            if (_inputButton.Word.Contains("DON"))
            {
               if(GameState.Instance != null)
               {
                   GameState.Instance.Win();
                   _inputButton.ResetListKey();
                   Debug.Log("DON");
               }
            }
        }
    }
}
