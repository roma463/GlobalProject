using UnityEngine;

[ExecuteAlways]
public class WirePath : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private bool _upLine;
    [SerializeField] private float _offsetLine;
    private Transform _endPosition;
    private Transform _begginPosition;

    [ExecuteAlways]
    private void Awake()
    {
        _endPosition = GetComponent<Button>().GetTransformActiveObject();
        _begginPosition = transform.parent;
        CreatePath();
    }
    [ExecuteAlways]
    private void CreatePath()
    {
        if (_endPosition != null && _begginPosition != null)
        {
            Vector3[] points = new Vector3[4];
            points[0] = (Vector2)_begginPosition.position;
            float x, y;
            x = _begginPosition.position.x - _endPosition.position.x;
            y = _begginPosition.position.y - _endPosition.position.y;
            if (_upLine)
            {
                points[1] = (Vector2)_begginPosition.position - Vector2.up * (y / 2) + _offsetLine * Vector2.up;
                points[2] = points[1] - Vector3.right * x;
            }
            else
            {
                points[1] = (Vector2)_begginPosition.position - Vector2.right * (x / 2) + _offsetLine * Vector2.right;
                points[2] = points[1] - Vector3.up * y;
            }
            points[3] = (Vector2)_endPosition.position;
            _lineRenderer.SetPositions(points);
        }
        else
        {
            _endPosition = GetComponent<Button>().GetTransformActiveObject();
            _begginPosition = transform.parent;
        }
    }
#if UNITY_EDITOR
    [ExecuteAlways]
    private void Update()
    {
        CreatePath();
    }
#endif
}
