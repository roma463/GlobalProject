using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(MusicWindow))]
public class AnimationMusic : MonoBehaviour
{
    [SerializeField] private Transform _pauseButton;
    [SerializeField] private float _speed;
    [SerializeField] private Volume _globalVolume;
    private ColorAdjustments _colorAdjustments;
    private MusicWindow _musicWindow;
    private const float _rotationIsPlay = -45, _rotationIsPause = 0;
    private bool _isAnimationPlaying = false;

    private void Awake()
    {
        _musicWindow = GetComponent<MusicWindow>();
        _globalVolume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
    }

    private void OnEnable()
    {
        _musicWindow.OnPlay += StartAnimation;
    }

    private void OnDisable()
    {
        _musicWindow.OnPlay -= StartAnimation;
    }

    private void StartAnimation(bool isPlaying)
    {
        var targetRotation = isPlaying ? _rotationIsPlay : _rotationIsPause;
        if (_isAnimationPlaying)
            StopAllCoroutines();
        Quaternion rotation = Quaternion.Euler(Vector3.forward * targetRotation);
        StartCoroutine(AnimationRotation(rotation));

        _colorAdjustments.saturation.value = isPlaying ? 0 : -50; 
    }

    private IEnumerator AnimationRotation(Quaternion targetRotation)
    {
        _isAnimationPlaying = true;
        while(_pauseButton.rotation.z != targetRotation.z)
        {
            _pauseButton.rotation = Quaternion.RotateTowards(_pauseButton.localRotation, targetRotation, _speed * Time.deltaTime);
            yield return null;
        }
        _isAnimationPlaying = false;
    }
}
