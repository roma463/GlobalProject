using UnityEngine;

public class UndependBlock : Undepend
{
    [SerializeField] private UsePlayerObject _usePlayerObject;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_usePlayerObject.UseNow)
            base.OnCollisionEnter2D(collision);
        ResetForceOnCollision();
    }

    public override void SetForce(GravityLine gravityLine)
    {
        base.SetForce(gravityLine);
        if (!_usePlayerObject.UseNow)
        {
            gravityLine.PlayCollision(_rigidbody2D);
        }
    }
}
