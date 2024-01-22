using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.TryGetComponent(out Rigidbody2D rigidbody))
            {
                AddForce(rigidbody);
            }
        }
        else if (collision.TryGetComponent(out Rigidbody2D rigidbody))
        {
            AddForce(rigidbody);
        }
    }
    private void AddForce(Rigidbody2D rigidbody)
    {
        var velocity = rigidbody.velocity;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(transform.up * velocity.y * -1, ForceMode2D.Impulse);
    }
}
