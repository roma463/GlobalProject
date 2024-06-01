using System;
using UnityEngine;
using UnityEngine.Events;

public class InputButton : MonoBehaviour
{
    public static InputButton Instance { get; private set; }
    public float Horizontal { private set; get; }
    public bool MouseRightStay { private set; get; }
    public bool MouseLeft { private set; get; }
    public bool Space { private set; get; }
    public bool Escape { private set; get; }
    public bool KeyR { private set; get; }
    public event UnityAction KeyE;

    public bool IsPause { private set; get; } = false;
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
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private string[] _knowKeyDown = new string[10];
    private string localWord;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        localWord = "";
        GameState.Instance.Initialize(this);
    }

    private void Update()
    {
        if (!IsPause)
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            KeyE?.Invoke();
        }

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
        IsPause = pause;
    }
}
