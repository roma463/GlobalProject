using UnityEngine;
using UnityEngine.Events;

public class EventCameraChanged : MonoBehaviour
{
    [SerializeField] private UnityEvent e_enterCollisionZoneCamera;
    [SerializeField] private UnityEvent e_exitCollisionZoneCamera;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerCollision playerCollisiton))
        {
            e_exitCollisionZoneCamera?.Invoke();
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerCollision playerCollision))
        {
            e_enterCollisionZoneCamera?.Invoke();
        }
    }
}
