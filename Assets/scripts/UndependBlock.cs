using UnityEngine;

public class UndependBlock : Undepend
{
    [SerializeField] private UsePlayerObject _usePlayerObject;

    private void Start()
    {
        ChangeGravity();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_usePlayerObject.UseNow)
            base.OnCollisionEnter2D(collision);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out GravityLine gravityLine))
        {
            base.OnTriggerEnter2D(collision);
            if (!_usePlayerObject.UseNow)
            {
                gravityLine.PlayCollision(_rigidbody2D);
            }
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
