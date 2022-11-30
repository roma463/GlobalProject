using UnityEngine;

public class Undepend : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ParticleSystem _collisionGravityLine;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private Animator _animation;
    [Range(-1,1)]
    [SerializeField] private int _startGravityScale = 1;

    private float _force;
    private bool _ferstCollision;
    private bool _vectorForce;
    private float _vectorForceInt;

    private void Start()
    {
        _rigidbody2D.gravityScale = _startGravityScale;
        var scale = transform.localScale;
        scale.y *= _startGravityScale;
        transform.localScale = scale;

    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GravityLine gravityLine))
        {
            if (_ferstCollision == false)
                _force = _rigidbody2D.velocity.y;
            _ferstCollision = true;
            _vectorForceInt = _force;
            if (_vectorForce)
                _vectorForceInt *= -1;

            _vectorForce = !_vectorForce;
            _collisionGravityLine.Play();
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out GravityLine gravityLine))
        {
            _rigidbody2D.gravityScale *= -1;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _vectorForceInt);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_animation != null)
            _animation.SetTrigger("Start");
        _hit.volume = collision.relativeVelocity.magnitude * 0.05f;
        _hit.Play();

    }
    public void ResetForceOnTrigger()
    {
        _ferstCollision = false;
        _vectorForce = false;
    }
}
