using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Teleport _teleport;
    [SerializeField] private BulletDisableParticle _effects;
    [SerializeField] private Rigidbody2D _rigidbody;

    public void Init(Teleport teleport, Vector2 startForce)
    {
        _rigidbody.gravityScale = teleport.GravityScale;
        _rigidbody.AddForce(startForce, ForceMode2D.Impulse);
        _teleport = teleport;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.velocity = Vector2.zero;
        }
        if (collision.gameObject.TryGetComponent(out CollisionSurface collsionSurface))
        {
            var normal = collision.GetContact(0).normal;
            var TeleportNormal = NormalTeleport(normal);

            TeleportPlayer(TeleportNormal, collision.GetContact(0).point);
        }
        DestroyBullet();
    }

    public virtual Teleport.Offset NormalTeleport(Vector2 normalSurface)
    {
        Teleport.Offset normal = new Teleport.Offset();
        if (normalSurface == Vector2.up)
        {
            normal = Teleport.Offset.up;
        }
        else if (normalSurface == -Vector2.up)
        {
            normal = Teleport.Offset.down;
        }
        if (normalSurface == Vector2.right)
        {
            normal = Teleport.Offset.right;
        }
        else if (normalSurface == -Vector2.right)
        {
            normal = Teleport.Offset.left;
        }
        return normal;
    }

    public virtual void TeleportPlayer(Teleport.Offset normal, Vector2 positionCollision)
    {
        _teleport.Player(normal, positionCollision);
    }

    public virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EffectDie();
    }

    public void EffectDie()
    {
        _effects.transform.parent = null;
        _effects.StopParticals();
    }
}
