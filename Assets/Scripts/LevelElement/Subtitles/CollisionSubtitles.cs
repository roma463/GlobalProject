using UnityEngine;

[RequireComponent(typeof(Dialog))]
public class CollisionSubtitles : MonoBehaviour
{
    private Dialog _dialog;
    private bool _isCollisionPlayer = false;
    [SerializeField] private BoxCollider2D _boxCollider;

    private void Start()
    {
        _dialog = GetComponent<Dialog>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerCollision playerCollision) && _isCollisionPlayer == false)
        {
            _isCollisionPlayer = true;
            _dialog.StartDialog();
        }
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
            return;
        Vector2 postion = _boxCollider.offset + (Vector2)transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(postion, _boxCollider.size);
    }
}
