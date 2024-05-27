using UnityEngine;

public class TerminalCollision : MonoBehaviour
{
    [SerializeField] private WindowUI _terminal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _terminal.Activate();
    }
}
