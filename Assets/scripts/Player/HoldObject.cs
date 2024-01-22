using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public bool ObjectRised { get; private set; }
    [SerializeField] private RelativeJoint2D _joint;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _pointKeep;
    [SerializeField] private LayerMask _holdObject;
    [SerializeField] private LayerMask _layerForRay;
    [SerializeField] private float _radiusCapture;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _spawnBullet;
    private UsePlayerObject _useObject;

    public void GameobjectKeep()
    {
        var collision = Physics2D.OverlapCircle(_pointKeep.position, _radiusCapture, _holdObject);
        if (collision == null)
            return;
        var hit = Physics2D.Linecast(_spawnBullet.position, collision.transform.position, _layerForRay);
        if (hit.collider.gameObject != collision.gameObject)
            return;
        if (collision.transform.parent.TryGetComponent(out UsePlayerObject use))
        {
            use.Reise();
            _joint.connectedBody = use.gameObject.GetComponent<Rigidbody2D>();
            _useObject = use;
            ObjectRised = true;
        }
    }
    public void TeleportCurrentUseObject(Vector2 position)
    {
        if (ObjectRised == true)
        {
            _useObject.transform.position = transform.position;
        }
    }
    public void Throw()
    {
        if (_useObject == null)
            return;
        _useObject.Put();
        _useObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,1) * _rigidbody2D.velocity;
        ObjectRised = false;
        _joint.connectedBody = null;
        _useObject = null;
    }
    private void Update()
    {
        if (ObjectRised == true)
        {
            if (Vector2.Distance(_useObject.transform.position, _pointKeep.position) > _maxDistance)
            {
                Throw();
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_pointKeep.position, _radiusCapture);
        if (_useObject != null)
            Gizmos.DrawLine(transform.position, _useObject.transform.position);
    }
#endif
}
