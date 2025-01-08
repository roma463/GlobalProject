using UnityEngine;

public class TerminalCollision : MonoBehaviour
{
    [SerializeField] private WindowUI _terminal;
    [SerializeField] private Hint _text;

    private void OnDisable()
    {
        InputButton.Instance.KeyE -= ActiveTerminal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InputButton.Instance.KeyE += ActiveTerminal;
        _text.AnimationShow();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InputButton.Instance.KeyE -= ActiveTerminal;
        _text.AnimationHide();
    }

    public void ActiveTerminal()
    {
        InputButton.Instance.KeyE -= ActiveTerminal;
        InputButton.Instance.KeyE += GameState.Instance.Win;
        _terminal.Show();
    }

}
