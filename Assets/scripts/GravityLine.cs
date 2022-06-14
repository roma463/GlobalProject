using UnityEngine;

public class GravityLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private AudioSource _collision;
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

                _collision.volume = rigidbody.velocity.magnitude * 0.01f;
                _collision.Play();

                if (collisionObj.gameObject == Teleport.GlobaTP.gameObject)
                {
                    Teleport.GlobaTP.UpdateDataGraviryScale(rigidbody);
                }
            }
        }
       
    }
}
