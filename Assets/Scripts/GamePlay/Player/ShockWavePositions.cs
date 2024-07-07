using System.Collections;
using UnityEngine;

public class ShockWavePositions : MonoBehaviour
{
    public static ShockWavePositions Instance {  get; private set; }
    [SerializeField] private Material _shockWave;
    [SerializeField] private float _speedWave;
    [SerializeField] private float _timeOfAction;
    [SerializeField] private float _radiuseShockWave;
    [SerializeField] private float _forceShockWave = 0.3f;
    private Camera _camera;
    private float _hieghtScreen, _widthSceen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
        UpdateSizeScreen();
        _shockWave.SetFloat("_floatWave", 0);
    }

    public void UpdateSizeScreen()
    {
        _hieghtScreen = _camera.pixelHeight;
        _widthSceen = _camera.pixelWidth;
    }
    public IEnumerator Teleportation(Vector2 newPositionPlayer)
    {
        UpdateSizeScreen();

        float currentValue = 0;
        float currentTime = 0;
        float forceWave = _forceShockWave;
        float forceProcent = forceWave;

        var PlayerScreenPosition = _camera.WorldToScreenPoint(newPositionPlayer);
        Vector2 ShaderPosition = new Vector2();
        ShaderPosition.x = PlayerScreenPosition.x / _widthSceen;
        ShaderPosition.y = PlayerScreenPosition.y / _hieghtScreen;
        _shockWave.SetVector("_PlayerPosition", ShaderPosition);
        for (; currentTime <= _timeOfAction; )
        {
            var delaTime = Time.deltaTime;
            currentTime += delaTime;

            forceWave -= delaTime * forceProcent / _timeOfAction;
            forceWave = Mathf.Clamp(forceWave, 0, forceProcent);

            currentValue += delaTime * _radiuseShockWave / _timeOfAction * _speedWave;

            _shockWave.SetFloat("_Radius", currentValue);
            _shockWave.SetFloat("_floatWave", forceWave);
            yield return null;
        }
    }
}
