using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _collisionLayer;

    [SerializeField] private Transform _pointCollision;
    [SerializeField] private Vector2 _collisionSize;
    [SerializeField] private float _jumpDelayTime = 0.05f;
    [SerializeField] private float _timeGroundCheck = 0.1f;

    private Rigidbody2D _rigidbody;
    private InputButton _inputButton;
    private bool _isGround;
    private bool _canJump = true;
    private bool _startGroundCheckCoroutine;
    private bool _isCollisionEnabled = true;

    private void Start()
    {
        _inputButton = GetComponent<InputButton>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_inputButton.Horizontal * _speed, _rigidbody.velocity.y);
    }

    private void Update()
    {
        bool isGrounded = _isCollisionEnabled && Physics2D.OverlapBox(_pointCollision.position, _collisionSize, 0, _collisionLayer);

        if (_inputButton.Space && _canJump)
        {
            if (isGrounded)
            {
                Jump();
                StartCoroutine(DelayJump());
            }
            else if (_isGround)
            {
                Jump();
                StartCoroutine(DelayJump());
            }
        }

        if (isGrounded)
        {
            if (!_startGroundCheckCoroutine)
            {
                StopCoroutine(GroundCheck());
                _isGround = true;
                _startGroundCheckCoroutine = true;
            }
        }
        else if (_startGroundCheckCoroutine)
        {
            StartCoroutine(GroundCheck());
        }
    }

    private IEnumerator DelayJump()
    {
        _canJump = false;
        yield return new WaitForSeconds(_jumpDelayTime);
        _canJump = true;
    }

    public void DisableCollisionCheck()
    {
        StopCoroutine(GroundCheck());
        _isCollisionEnabled = false;
        Invoke(nameof(EnableCollisionCheck), 0.1f);
    }

    private void EnableCollisionCheck()
    {
        _isCollisionEnabled = true;
    }

    private void Jump()
    {
        _rigidbody.AddRelativeForce(new Vector2(0, _rigidbody.gravityScale) * _jumpForce);
    }

    private IEnumerator GroundCheck()
    {
        _startGroundCheckCoroutine = false;
        _isGround = true;

        for (float elapsedTime = 0f; elapsedTime < _timeGroundCheck; elapsedTime += Time.deltaTime)
        {
            if (_inputButton.Space || !_isCollisionEnabled)
            {
                _isGround = false;
                yield break;
            }
            yield return null;
        }

        _isGround = false;
    }
}