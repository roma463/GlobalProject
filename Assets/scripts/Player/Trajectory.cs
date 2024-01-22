using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private SpriteRenderer _pointCollisionLine;
    [SerializeField] private LayerMask _lineCollision;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private Vector2 _offsetTextTime;
    private bool _enabledLine = true;
    public void TrajectoryBullet(Vector2 velosity)
    {
        if (!_enabledLine)
        {
            return;
        }
        Vector3[] points = new Vector3[200];
        float time = 0;
        int index = 0;
        points[0] = (Vector2)_gunPoint.position;
        for (int i = 1; i < points.Length; i++)
        {
            time = i * .1f;
            points[i] = ((Vector2)_gunPoint.position + velosity * time + (Physics2D.gravity * Teleport.GlobalTP.GravityScale * Mathf.Pow(time, 2) / 2f));
            var hit = Physics2D.Raycast(points[i - 1], (points[i] - points[i - 1]).normalized, Vector2.Distance(points[i - 1], points[i]), _lineCollision);
            if (hit == true)
            {
                if (hit.collider.TryGetComponent(out CollisionSurface collision))
                {
                    _pointCollisionLine.color = Color.green;
                }
                else if (hit.collider.TryGetComponent(out GravityLine gravityLine))
                {
                    continue;
                }
                else
                {
                    _pointCollisionLine.color = Color.red;
                }
                _pointCollisionLine.transform.position = hit.point;
                _lineRenderer.positionCount = i;
                index = i / 2;
                break;
            }
        }
        _lineRenderer.SetPositions(points);
    }
    public void DisableTranjectoryLine()
    {
        _lineRenderer.enabled = false;
        _enabledLine = false;
        _pointCollisionLine.transform.position = transform.position;
    }
    public void EnableTranjectoryLine()
    {
        _lineRenderer.enabled = true;
        _enabledLine = true;
        _pointCollisionLine.transform.position = transform.position;
    }
}