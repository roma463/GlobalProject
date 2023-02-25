using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCinemaControl : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera[] _allCamerasInScene;
    private Cinemachine.CinemachineVirtualCamera _currentCamera;

    private void Start()
    {
        _currentCamera = _allCamerasInScene[0];
    }
    public void ChangeCurrentCamera(Cinemachine.CinemachineVirtualCamera camera)
    {
        _currentCamera.Priority = 1;
        _currentCamera = camera;
        _currentCamera.Priority = 2;
    }
}
