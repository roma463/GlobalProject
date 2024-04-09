using UnityEngine;

namespace GamePlay.Player
{
    public class RotateArm : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Rotation();
        }

        public virtual void Rotation()
        {
            var positionMouse = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var direction = transform.position - positionMouse;
            direction *= Teleport.GlobalTP.GravityScale;
            var rotateByZ = Quaternion.Euler( Vector3.forward  * -(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg));
            transform.rotation = rotateByZ;
        }
    }
}
