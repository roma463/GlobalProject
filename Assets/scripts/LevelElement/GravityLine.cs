using System.Collections;
using UnityEngine;

public class GravityLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private AudioSource _collision;
    [SerializeField] private float _delay;
    private bool _startSound = true;

    public void PlayCollision(Rigidbody2D rigidbody)
    {
        if (_startSound)
        {
            StartCoroutine(SoundPouse());
            _collision.volume = rigidbody.velocity.magnitude * 0.01f;
            _collision.Play();
        }
    }
    private IEnumerator SoundPouse()
    {
        _startSound = false;
        yield return new WaitForSeconds(_delay);
        _startSound = true;
    }
}
