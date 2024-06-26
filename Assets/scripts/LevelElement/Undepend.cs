using System.Collections;
using UnityEngine;

public class Undepend : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] private ParticleSystem _collisionGravityLine;
    [SerializeField] private AudioSource _hit;
    [Range(-1,1)]
    [SerializeField] private int _startGravityDirection = 1;
    [SerializeField] private float _delayCollision = 0.1f;
    private const float _minForce = 5;

    private bool _isCollision = false;
    private int _currentDirection;
    private float _velosityCollisiton;
    private bool _resetVelosity;

    private void Start()
    {
        _rigidbody2D.gravityScale = _startGravityDirection;
        _currentDirection = _startGravityDirection;
        var scale = transform.localScale;
        scale.y *= _startGravityDirection;
        transform.localScale = scale;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out GravityLine gravityLine))
        {
            SetForce(gravityLine);
        }
    }

    public virtual void SetForce(GravityLine gravityLine)
    {
        if (_isCollision)
            return;

        if (_resetVelosity == false)
        {
            if (Mathf.Abs(_rigidbody2D.velocity.y) < _minForce)
                _velosityCollisiton = _minForce * (_currentDirection * -1);
            else
                _velosityCollisiton = _rigidbody2D.velocity.y;

            _resetVelosity = true;
        }
        _collisionGravityLine.Play();
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GravityLine gravityLine))
        {
            if (_isCollision)
                return;

            _currentDirection *= -1;
            StartCoroutine(TimerDelayCollision());

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _velosityCollisiton);
            _rigidbody2D.gravityScale = _currentDirection;
            _velosityCollisiton *= -1;
        }
    }

    private IEnumerator TimerDelayCollision()
    {
        _isCollision = true;
        yield return new WaitForSeconds(_delayCollision);
        _isCollision = false;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        _hit.volume = collision.relativeVelocity.magnitude * 0.05f;
        _hit.Play();
    }

    public void ResetForceOnTrigger()
    {
        _resetVelosity = false;
    }
}
