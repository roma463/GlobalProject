using UnityEngine;

public class OnDisableObject : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _camera;
    [SerializeField] private CurrentCinemaControl _currentCinemaControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentCinemaControl.ChangeCurrentCamera(_camera);
    }
}
