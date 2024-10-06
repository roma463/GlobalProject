using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource _soundFinish;
    private GameState _gameState;

    private void Start()
    {
        _gameState = GameState.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent(out PlayerCollision player))
        {
            WinPlayer();
        }
    }

    public virtual void WinPlayer()
    {
        _gameState.Win();
    }
}
