using System.Collections;
using UnityEngine;

public class DinumicSurface : UsePlayerObject
{
    [SerializeField] private Transform _blackObj; 
    [SerializeField] private Transform _changeScale;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private BoxCollider2D _notCollisionSerface;
    private Vector2 _startScale;
    private void Start()
    {
        _startScale = _changeScale.localScale;
    }
    public override void Reise()
    {
        StartCoroutine(Open(new Vector3(0.1f, 0.1f, 0), false));
    }
    public override void Put()
    {
        StartCoroutine(Open(_startScale, true));
    }
    private IEnumerator Open(Vector3 TargetScele, bool enabledCollider)
    {
        _boxCollider2D.enabled = enabledCollider;
        while (_changeScale.localScale != TargetScele)
        {
            print("Corutine");
            _changeScale.localScale = Vector3.MoveTowards(_changeScale.localScale, TargetScele, .1f);
            yield return null;
        }
        _changeScale.localScale = TargetScele;
    }
}
