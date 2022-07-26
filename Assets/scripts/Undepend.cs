using UnityEngine;

public class Undepend : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ParticleSystem _collisionGravityLine;
    [SerializeField] private AudioSource _hit;

    private float _force;
    private bool _ferstCollision;
    private bool _vectorForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GravityLine gravityLine))
        {
            if (_ferstCollision == false)
                _force = _rigidbody2D.velocity.y;
            _ferstCollision = true;
            var x = _force;
            if (_vectorForce)
                x *= -1;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, x);
            _vectorForce = !_vectorForce;
            _collisionGravityLine.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hit.volume = collision.relativeVelocity.magnitude * 0.05f;
        _hit.Play();

    }
    public void ResetForceOnTrigger()
    {
        _ferstCollision = false;
        _vectorForce = false;
    }
}
