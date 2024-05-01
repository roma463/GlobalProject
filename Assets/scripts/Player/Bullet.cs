using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Teleport _teleport;
    [SerializeField] protected MoveBullet _moveBullet;
    [SerializeField] private BulletDisableParticle _effects;
    [SerializeField] private LayerMask _layerMask;

    public void Init(Teleport teleport, Vector2 velosity)
    {
        _teleport = teleport;
        _moveBullet.Initialize(velosity, PhotonNetwork.Time);
    }

    private void OnEnable()
    {
        _moveBullet.ChangePosition += CollisionLine;
    }

    private void OnDisable()
    {
        _moveBullet.ChangePosition -= CollisionLine;
    }

    public virtual void CollisionLine(Vector2 origin, Vector2 ending)
    {
        var hit = new RaycastHit2D();
        var directionLine = (ending - origin).normalized;

        hit = Physics2D.Raycast(origin, directionLine, Vector2.Distance(origin, ending), _layerMask);
        CheckCollisionCollider(hit);
    }

    public virtual void CheckCollisionCollider(RaycastHit2D hit)
    {
        if (hit)
        {
            if (hit.collider.TryGetComponent(out CollisionSurface collsionSurface))
            {
                CollisionSurface(hit.normal, hit.point);
            }
            DestroyBullet();
        }
    }

    public void CollisionSurface(Vector2 normalSurface, Vector2 positionCollision)
    {
        var TeleportNormal = NormalTeleport(normalSurface);
        TeleportPlayer(TeleportNormal, positionCollision);
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
        //Destroy(gameObject);
        gameObject.SetActive(false);
        EffectDie();
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
