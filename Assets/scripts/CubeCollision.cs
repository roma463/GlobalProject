using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    [SerializeField] private Undepend _undepend;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _undepend.ResetForceOnTrigger();
    }
}
