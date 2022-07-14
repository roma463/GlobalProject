using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private DisableParticle _train;
    [SerializeField] private GameObject _point;
    private Teleport _teleport;
    public void StartSimulate(PhysicsSimulations simulations)
    {
        //transform.parent = null;
        //simulations.AddObjectInScene(_point);
    }
    private void Start()
    {
        _teleport = Teleport.GlobaTP;
    }
    //private void FixedUpdate()
    //{
    //    transform.position = _point.transform.position;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CollisionSurface collsionSurface))
        {
            var narmal = collision.GetContact(0).normal;
                Teleport.Offset a = new Teleport.Offset();
                if (narmal == Vector2.up)
                {
                    a = Teleport.Offset.up;
                }
                else if (narmal == -Vector2.up)
                {
                    a = Teleport.Offset.down;
                }
                if (narmal == Vector2.right)
                {
                    a = Teleport.Offset.right;
                }
                else if (narmal == -Vector2.right)
                {
                    a = Teleport.Offset.left;
                }
                _teleport.Player(a, collision.GetContact(0).point);
        }
        _train.transform.parent = null;
        _train.StopParticals();
        Destroy(gameObject);
        Destroy(_point);
    }
}
