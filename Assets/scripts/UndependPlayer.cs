using UnityEngine;

public class UndependPlayer : Undepend
{
    [SerializeField] private Transform _pointUp;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Vector2 _sizeBox;
    [SerializeField] private HoldObject _holdObject;
    private void Update()
    {
       if(OverlapBox(_pointUp.position) || OverlapBox(_pointDown.position))
        {
            ResetForceOnTrigger();
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.TryGetComponent(out GravityLine gravityLine))
        {
            _holdObject.ChangeGravityHoldObject();
        }
    }
    private bool OverlapBox(Vector2 point)
    {
        return Physics2D.OverlapBox(point, _sizeBox, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_pointUp.position, _sizeBox);
        Gizmos.DrawWireCube(_pointDown.position, _sizeBox);
        
    }
}
