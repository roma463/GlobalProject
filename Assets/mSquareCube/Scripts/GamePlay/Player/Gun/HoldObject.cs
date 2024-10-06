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

    public void CheckHoldObject()
    {
        var collision = Physics2D.OverlapCircle(_pointKeep.position, _radiusCapture, _holdObject);
        if (collision == null)
            return;

        var hit = Physics2D.Linecast(_spawnBullet.position, collision.transform.position, _layerForRay);
        if (hit.collider.gameObject != collision.gameObject)
            return;

        if (collision.transform.parent.TryGetComponent(out UsePlayerObject use))
        {
            KeepingObject(use);
        }
    }

    public virtual void KeepingObject(UsePlayerObject UseObject)
    {
        UseObject.Take();
        _joint.connectedBody = UseObject.gameObject.GetComponent<Rigidbody2D>();
        _useObject = UseObject;
        ObjectRised = true;
    }

    public void TeleportCurrentUseObject()
    {
        if (ObjectRised == true)
        {
            _useObject.transform.position = transform.position;
        }
    }

    public virtual void Throw()
    {
        if (_useObject == null)
            return;
        _useObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * _rigidbody2D.velocity;
        _useObject.Put();
        ObjectRised = false;
        _joint.connectedBody = null;
        _useObject = null;
    }

    public virtual void Update()
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
