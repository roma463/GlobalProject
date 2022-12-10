using System.Collections;
using UnityEngine;

public class ShockWavePositions : MonoBehaviour
{
    [SerializeField] private Material _shockWave;
    [SerializeField] private float _speedWave;
    [SerializeField] private float _timeOfAction;
    [SerializeField] private float _radiuseShockWave;
    [SerializeField] private float _forceShockWave = 0.3f;
    private Transform _player;
    private Camera _camera;
    private float _hieghtScreen, _widthSceen;
    private void Start()
    {
        Teleport.GlobalTP.SetShockWave(this);
        _player = Teleport.GlobalTP.transform;
        _camera = Camera.main;
        _hieghtScreen = _camera.pixelHeight;
        _widthSceen = _camera.pixelWidth;
    }

    public IEnumerator Teleportation()
    {
        float currentValue = 0;
        float currentTime = 0;
        float forceWave = _forceShockWave;
        float forceProcent = forceWave;

        for (; currentTime <= _timeOfAction; )
        {
            var x = Time.deltaTime;
            currentTime += x;

            forceWave -= x * forceProcent / _timeOfAction;
            forceWave = Mathf.Clamp(forceWave, 0, forceProcent);
            
            var PlayerScreenPosition = _camera.WorldToScreenPoint(_player.position);
            Vector2 ShaderPosition = new Vector2();
            ShaderPosition.x = PlayerScreenPosition.x / _widthSceen;
            ShaderPosition.y = PlayerScreenPosition.y / _hieghtScreen;
            currentValue += x * _radiuseShockWave / _timeOfAction * _speedWave;

            _shockWave.SetFloat("_Radius", currentValue);
            _shockWave.SetFloat("_floatWave", forceWave);
            _shockWave.SetVector("_PlayerPosition", ShaderPosition);
            yield return null;
        }
    }
}
