using UnityEngine;

public class TerminalCollision : MonoBehaviour
{
    [SerializeField] private WindowUI _terminal;
    [SerializeField] private Hint _text;

    private void OnEnable()
    {
        InputButton.Instance.KeyE += ActiveTerminal;
    }

    private void OnDisable()
    {
        InputButton.Instance.KeyE -= ActiveTerminal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _text.AnimationShow();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _text.AnimationHide();
    }

    public void ActiveTerminal()
    {
        _terminal.Activate();
    }

}
