using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource _soundFinish;
    private GameState _gameState;

    private void Start()
    {
        _gameState = GameState.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCollision player))
        {
            WinPlayer();
        }
    }

    public virtual void WinPlayer()
    {
            _gameState.Win();
            _soundFinish.Play();
    }
}
