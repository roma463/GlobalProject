using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    [SerializeField] private Undepend _undepend;
    [SerializeField] private Animator _animation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_animation != null)
            _animation.SetTrigger("Start");
        _undepend.ResetForceOnTrigger();
    }
}
