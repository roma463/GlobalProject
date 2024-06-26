using UnityEngine;

public class UndependPlayer : Undepend
{
    [SerializeField] private Transform _pointUp;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Vector2 _sizeBox;
    [SerializeField] private HoldObject _holdObject;
    [SerializeField] private Teleport _teleport;

    private void Update()
    {
       if(OverlapBox(_pointUp.position) || OverlapBox(_pointDown.position))
        {
            ResetForceOnTrigger();
        }
    }

    public override void SetForce(GravityLine gravityLine)
    {
        base.SetForce(gravityLine);
        var scale = transform.localScale;
        scale.y *= -1;
        scale.x *= -1;
        transform.localScale = scale;

        gravityLine.PlayCollision(_rigidbody2D);
        _teleport.UpdateDataGraviryScale(_rigidbody2D);
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
