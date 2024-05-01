using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public event Action<Vector2, Vector2> ChangePosition;
    private float _timeCreate;

    public void Initialize(Vector2 velocity, double timeCreate)
    {
        _timeCreate = (float)timeCreate;
        StartCoroutine(Movement(velocity));
    }

    private IEnumerator Movement(Vector2 velocity)
    {
        float time = 0;
        var startPosition = (Vector2)transform.position;
        while (true)
        {
            var newPosition = startPosition + velocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
            ChangePosition?.Invoke(transform.position, newPosition);
            transform.position = newPosition;
            time = (float)(PhotonNetwork.Time - _timeCreate);
            yield return null;
        }
    }
}
