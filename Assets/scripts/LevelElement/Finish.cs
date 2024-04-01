using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameUi _ui;
    [SerializeField] private AudioSource _soundFinish;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCollision player))
        {
            _ui.Win();
            _soundFinish.Play();
        }
    }
}
