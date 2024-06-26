using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private SpriteRenderer _pointCollisionLine;
    [SerializeField] private LayerMask _lineCollision;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private Vector2 _offsetTextTime;
    [SerializeField] private Teleport _teleport;
    private bool _enabledLine = true;
    private const float TimeStep = 0.1f;

    public void TrajectoryBullet(Vector2 velocity)
    {
        if (!_enabledLine)
            return;

        Vector3[] points = new Vector3[200];
        float time = 0;
        points[0] = (Vector2)_gunPoint.position;

        for (int i = 1; i < points.Length; i++)
        {
            time = i * TimeStep;
            points[i] = ((Vector2)_gunPoint.position + velocity * time + (Physics2D.gravity * _teleport.GravityScale * Mathf.Pow(time, 2) / 2f));

            var hit = GroundCheck(points[i-1], points[i]);
            if (hit == true)
            {
                points[i] = hit.point;
                _pointCollisionLine.transform.position = hit.point;
                _lineRenderer.positionCount = i + 1;
                break;
            }
        }
        _lineRenderer.SetPositions(points);
    }

    private RaycastHit2D GroundCheck(Vector2 startRay, Vector2 endRay)
    {
        var hit = Physics2D.Raycast(startRay, (endRay - startRay).normalized, Vector2.Distance(startRay, endRay), _lineCollision);
        if (hit == true)
        {
            if (hit.collider.TryGetComponent(out CollisionSurface collision))
            {
                _pointCollisionLine.color = Color.green;
            }
            else
            {
                _pointCollisionLine.color = Color.red;
            }
        }
        return hit;
    }

    public void DisableTrajectoryLine()
    {
        _lineRenderer.enabled = false;
        _enabledLine = false;
        _pointCollisionLine.transform.position = transform.position;
    }

    public void EnableTrajectoryLine()
    {
        _lineRenderer.enabled = true;
        _enabledLine = true;
        _pointCollisionLine.transform.position = transform.position;
    }
}
