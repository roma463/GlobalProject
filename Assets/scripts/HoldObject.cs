using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public enum StateMove
    {
        moveObject,
        teleportPLayer
    }
    public StateMove StateTP { get; private set; }
    [SerializeField] private RelativeJoint2D _joint;
    [SerializeField] private Transform _pointKeep;
    [SerializeField] private LayerMask _holdObject;
    [SerializeField] private LayerMask _layerForRay;
    [SerializeField] private float _radiusCapture;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _spawnBullet;
    private Transform _useObject;
    private void Awake()
    {
        StateTP = StateMove.teleportPLayer;
    }
    public void GameobjectKeep()
    {
        var collision = Physics2D.OverlapCircle(_pointKeep.position, _radiusCapture, _holdObject);
        if (collision == null)
            return;
        var hit = Physics2D.Linecast(_spawnBullet.position, collision.transform.position, _layerForRay);
        if (hit.collider.gameObject != collision.gameObject)
            return;
        if (collision.TryGetComponent(out UseObject use))
        {
            _joint.connectedBody = use.gameObject.GetComponent<Rigidbody2D>();
            _useObject = collision.transform;
            StateTP = StateMove.moveObject;
        }
    }
    public void TeleportCurrentUseObject(Vector2 position)
    {
        if (StateTP == StateMove.moveObject)
        {
            _useObject.position = transform.position;
        }
    }
    public void Throw()
    {
        if (_useObject == null)
            return;
        _useObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StateTP = StateMove.teleportPLayer;
        _joint.connectedBody = null;
        _useObject = null;
    }
    private void Update()
    {
        if (StateTP == StateMove.moveObject)
        {
            if (Vector2.Distance(_useObject.position, _pointKeep.position) > _maxDistance)
            {
                StateTP = StateMove.teleportPLayer;
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
