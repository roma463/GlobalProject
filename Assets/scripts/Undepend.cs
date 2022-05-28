using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undepend : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _force;
    bool _ferstCollision;
    bool _vectorForce;
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResetForceOnTregger();
    }
    public void ResetForceOnTregger()
    {
        _ferstCollision = false;
        _vectorForce = false;
    }
}
