using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private Vector2 velosity;
    [SerializeField] private LineRenderer _lineRenderer;
    private void Update()
    {
        Vector3[] points = new Vector3[50];
        var localVelosity = velosity;
        points[0] = (Vector2)_gunPoint.position;
        Vector2 gravity = Physics2D.gravity;
        for (int i = 1; i < points.Length; i++)
        {
            float time = i * .05f;
            points[i] = ((Vector2)_gunPoint.position + (localVelosity) * time + (gravity * Mathf.Pow(time, 2) / 2f));
            var hit = Physics2D.Raycast(points[i - 1], (points[i] - points[i - 1]).normalized, Vector2.Distance(points[i - 1], points[i]));
            if (hit == true)
            {
                if (hit.collider.TryGetComponent(out GravityLine collision))
                {
                   localVelosity = Vector2.up * Mathf.Pow(gravity.y * time,2) + velosity * time + (Vector2)_gunPoint.position;
                print(localVelosity);
                    localVelosity.y *= -1;
                }
               
            }
        }
        _lineRenderer.positionCount = 50;
        _lineRenderer.SetPositions(points);
    }
}
