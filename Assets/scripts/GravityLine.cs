using UnityEngine;

public class GravityLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj;
        if (collision.transform.parent != null)
        {
            collisionObj = collision.transform.parent.gameObject;

            if (collisionObj.TryGetComponent(out Rigidbody2D rigidbody))
            {
                var scale = collisionObj.transform.localScale;
                scale.y *= -1;
                scale.x *= -1;
                collisionObj.transform.localScale = scale;
                rigidbody.gravityScale *= -1;

                if (collisionObj.gameObject == Teleport.GlobaTP.gameObject)
                {
                    Teleport.GlobaTP.UpdateDataGraviryScale(rigidbody);
                }
            }
        }
       
    }
}
