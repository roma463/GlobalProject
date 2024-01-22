using UnityEngine;

public class TriggerCreate : MonoBehaviour
{
    [SerializeField] private Animator _createRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCollider")
        {
            _createRoom.SetTrigger("Start");
        }
    }
}
