using UnityEngine;

[RequireComponent(typeof(Dialog))]
public class CollisionSubtitles : MonoBehaviour
{
    private Dialog _dialog;
    private bool _isCollisionPlayer = false;

    private void Start()
    {
        _dialog = GetComponent<Dialog>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out PlayerCollision playerCollision) && _isCollisionPlayer == false)
        {
            _isCollisionPlayer = true;
            _dialog.StartDialog();
        }
    }
}
