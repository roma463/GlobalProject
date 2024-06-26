using UnityEngine;

public class ViewBoxCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Color _color;

    private void OnDrawGizmos()
    {
        if(_boxCollider != null)
        {
            Gizmos.color = _color;
            Vector2 postion = _boxCollider.offset + (Vector2)transform.position;
            Gizmos.DrawWireCube(postion, _boxCollider.size);
        }
    }
}
