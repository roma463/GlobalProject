using System;
using System.Collections;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public event Action<Vector2, Vector2> ChangePosition;
    private float _timeCreate;
    private int _gravityScale;

    public void Initialize(Vector2 velocity, int gravityScale)
    {
        _timeCreate = Time.time;
        _gravityScale = gravityScale;
        StartCoroutine(Movement(velocity));
    }

    public void StopMovement()
    {
        StopAllCoroutines();
    }

    private IEnumerator Movement(Vector2 velocity)
    {
        float time = 0;
        var startPosition = (Vector2)transform.position;
        while (true)
        {
            var newPosition = startPosition + velocity * time + 0.5f * (Physics2D.gravity * _gravityScale) * Mathf.Pow(time, 2);
            ChangePosition?.Invoke(transform.position, newPosition);
            transform.position = newPosition;
            time = Time.time - _timeCreate;
            yield return null;
        }
    }
}
