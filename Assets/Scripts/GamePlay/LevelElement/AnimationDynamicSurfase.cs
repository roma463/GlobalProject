using System.Collections;
using UnityEngine;

public class AnimationDynamicSurfase : MonoBehaviour
{
    [SerializeField] private UsePlayerObject _useObject;
    [SerializeField] private Transform _changeScale;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private ParticleSystem _scales;
    [SerializeField] private float _speedOpen = .1f;
    private Vector2 _startScale;

    private void Start()
    {
        _startScale = _changeScale.localScale;
    }

    private void OnEnable()
    {
        _useObject.e_take += TakeAnimation;
        _useObject.e_put += PutAnimation;
    }

    private void OnDisable()
    {
        _useObject.e_take -= TakeAnimation;
        _useObject.e_put -= PutAnimation;
    }

    private void TakeAnimation()
    {
        PlayParticleFX();
        StopAllCoroutines();
        StartCoroutine(ChangeScale(new Vector3(_speedOpen, _speedOpen, 0), false));
    }

    private void PutAnimation()
    {
        PlayParticleFX();
        StopAllCoroutines();
        StartCoroutine(ChangeScale(_startScale, true));
    }

    private void PlayParticleFX()
    {
        _scales.startSpeed *= -1;
        _scales.Play();
    }

    private IEnumerator ChangeScale(Vector3 TargetScele, bool enabledCollider)
    {
        _boxCollider2D.enabled = enabledCollider;
        while (_changeScale.localScale != TargetScele)
        {
            _changeScale.localScale = Vector3.MoveTowards(_changeScale.localScale, TargetScele, _speedOpen * Time.deltaTime);
            yield return null;
        }
        _changeScale.localScale = TargetScele;
    }
}
