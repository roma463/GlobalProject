using System;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    public float Horizontal { private set; get; }
    public bool MouseRightStay { private set; get; }
    public bool MouseLeft { private set; get; }
    public bool Space { private set; get; }
    public bool Escape { private set; get; }
    public bool KeyR { private set; get; }

    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private string[] _knowKeyDown = new string[10];
    private string localWord;
    public string Word 
    { 
        get 
        {
            return localWord; 
        }
        private set 
        {
            localWord =  value; 
        } 
    }
    private bool _isPause = true;
    private void Awake()
    {
        localWord = "";
        //Cursor.visible = false;
    }
    private void Update()
    {
        if (_isPause)
        {
        Horizontal = Input.GetAxis("Horizontal");

        if (Input.GetMouseButtonDown(1))
            MouseRightStay = true;
        else if (Input.GetMouseButtonUp(1))
            MouseRightStay = false;

        MouseLeft = Input.GetMouseButtonDown(0);
        Space = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            Horizontal = 0;
        }
        Escape = Input.GetKeyDown(KeyCode.Escape);
        KeyR = Input.GetKeyDown(KeyCode.R);
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    ArrayOffset();
                    _knowKeyDown[0] = keyCode.ToString();

                    localWord = "";
                    for (int i = _knowKeyDown.Length - 1; i >= 0; i--)
                    {
                        localWord += _knowKeyDown[i];
                    }
                }
            }
        }
    }
    public void ResetListKey()
    {
        for (int i = 0; i < _knowKeyDown.Length; i++)
        {
            _knowKeyDown[i] = "";
        }
    }
    private void ArrayOffset()
    {
        string valueCell = _knowKeyDown[0];
        for (int i = 0; i < _knowKeyDown.Length-1; i++)
        {
            string timevalue = _knowKeyDown[i + 1];
            _knowKeyDown[i + 1] = valueCell;
            valueCell = timevalue;
        }
        
    }
    public void Pause(bool pause)
    {
        _isPause = pause;
    }
}
