using UnityEngine;

public class WirePath : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    private Transform _endPosition;
    private void Awake()
    {
        PlayerPrefs.DeleteKey("Scene");
        _endPosition = GetComponent<Button>().GetTransformActiveObject();
        Vector3[] points = new Vector3[4];
        points[0] = transform.position;
        float x,y;
        if((x = transform.position.x-_endPosition.position.x) > (y = transform.position.y - _endPosition.position.y))
        {
            points[1] = (Vector2)transform.position - Vector2.up * (y / 2);
            points[2] = points[1] - Vector3.right * x;
        }
        else
        {

            points[1] = (Vector2)transform.position - Vector2.right * (x / 2);
            points[2] = points[1] - Vector3.up * y;
        }
        points[3] = _endPosition.position;
        _lineRenderer.SetPositions(points);
    }
}
