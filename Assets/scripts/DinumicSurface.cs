using System.Collections;
using UnityEngine;

public class DinumicSurface : UsePlayerObject
{
    [SerializeField] private Transform _blackObj; 
    [SerializeField] private Transform _changeScale;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private BoxCollider2D _notCollisionSerface;
    [SerializeField] private ParticleSystem _scales;
    [SerializeField] private float _speedOpen = .1f;
    private Vector2 _startScale;
    private void Start()
    {
        _startScale = _changeScale.localScale;
    }
    public override void Reise()
    {
        _scales.startSpeed *= -1;
        _scales.Play();
        base.Reise();
        StopAllCoroutines();
        StartCoroutine(Open(new Vector3(0.1f, 0.1f, 0), false));
    }
    public override void Put()
    {
        _scales.startSpeed *= -1;
        _scales.Play();
        base.Put();
        StopAllCoroutines();
        StartCoroutine(Open(_startScale, true));
    }
    private IEnumerator Open(Vector3 TargetScele, bool enabledCollider)
    {
        _boxCollider2D.enabled = enabledCollider;
        while (_changeScale.localScale != TargetScele)
        {
            print("Corutine");
            _changeScale.localScale = Vector3.MoveTowards(_changeScale.localScale, TargetScele, _speedOpen * Time.deltaTime);
            yield return null;
        }
        _changeScale.localScale = TargetScele;
    }
}
