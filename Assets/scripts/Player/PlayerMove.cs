using Photon.Pun;
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
    [SerializeField] private float _timeKnowJump;
    [SerializeField] private float _jumpDelayTime = 0.05f;
    private bool _delayJumpEnd = true;

    private Rigidbody2D _rigidbody;
    private InputButton _inputButton;
    private bool _isGround;
    private bool _InputJump;
    private bool _startCorutaine;
    private bool _onCollison = true;

    private void Start()
    {
        _inputButton = GetComponent<InputButton>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = (new Vector2(_inputButton.Horizontal * _speed, _rigidbody.velocity.y));
    }
    private void Update()
    {
        var collsionGround = false;
        if (_onCollison)
        {
            collsionGround = Physics2D.OverlapBox(_pointCollision.position, _collisionSize,0, _collisionLayer);
        }
        if (_inputButton.Space && _delayJumpEnd)
        {
            _InputJump = true;
            if (collsionGround == false)
            {
                if (_isGround)
                {
                    Jump();
                    StartCoroutine(DelayJump());
                }
            }
            else
            {
                StartCoroutine(DelayJump());
                Jump();
            }
        }
        if (collsionGround && !_startCorutaine)
        {
            StopCoroutine(Knowjump());
            _isGround = true;
            _InputJump = false;
            _startCorutaine = true;
        }
        else if (!collsionGround && _startCorutaine)
        {
            StartCoroutine(Knowjump());
        }
    }
    private IEnumerator DelayJump()
    {
        _delayJumpEnd = false;
        yield return new WaitForSeconds(_jumpDelayTime);
        _delayJumpEnd = true;
    }
    public void UnplugJump()
    {
        StopCoroutine(Knowjump());
        _onCollison = false;
        Invoke(nameof(ChengeBool), .1f);
    }
    private void ChengeBool()
    {
        _onCollison = true;
    }
    private void Jump()
    {
        _rigidbody.AddRelativeForce(new Vector2(0, _rigidbody.gravityScale) * _jumpForce);
    }
    private IEnumerator Knowjump()
    {
        _startCorutaine = false;
        _isGround = true;
        for (float i = 0; i < _timeKnowJump; i += Time.deltaTime)
        {
            if (_InputJump || _onCollison == false)
            {
                _isGround = false;
                yield break;
            }
            yield return null;
        }
        _isGround = false;
    }
}