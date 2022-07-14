using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : MonoBehaviour, Action
{
    [SerializeField] private bool _startActive;
    [Range(0, 1)]
    [SerializeField] private float blockout = 0.3f;
    [Range(0, 1)]
    [SerializeField] private float _changedInFrame = 0.05f;
    private bool _currentActivity;
    private List<OblectInformation> _allChildrenObjects = new List<OblectInformation>();

    private void Start()
    {
        _currentActivity = _startActive;
        var getCheldrensSpriteSenderer = GetComponentsInChildren<SpriteRenderer>();
        var getCheldrensBoxCollider = GetComponentsInChildren<BoxCollider2D>();
        for (int i = 0; i < getCheldrensBoxCollider.Length; i++)
        {
            _allChildrenObjects.Add(new OblectInformation(getCheldrensSpriteSenderer[i], getCheldrensBoxCollider[i]));
        }
        ActiveThisObject(_startActive);
    }
    public void launch()
    {
        _currentActivity = !_currentActivity;
        ActiveThisObject(_currentActivity);
    }
    private void ActiveThisObject(bool stateActive)
    {
                StopAllCoroutines();
        int VectorDirectionColor = stateActive ? 1 : -1;
        for (int i = 0; i < _allChildrenObjects.Count; i++)
        {
            _allChildrenObjects[i].ColliderEnable(stateActive);
            Color.RGBToHSV(_allChildrenObjects[i].spriteRenderer.color, out float H, out float S, out float V);

            float targetColor = V + blockout * VectorDirectionColor;
            targetColor = Mathf.Clamp(targetColor, _allChildrenObjects[i].StartColor-blockout, _allChildrenObjects[i].StartColor);

            StartCoroutine(ColorByClick(_allChildrenObjects[i].spriteRenderer, _changedInFrame * VectorDirectionColor, targetColor));
        }
    }
    private IEnumerator ColorByClick(SpriteRenderer spriteRenderer, float change, float targetColor)
    {
        Color.RGBToHSV(spriteRenderer.color, out float H, out float S, out float V);
        var _currentColor = V;
        while (change > 0 ? targetColor > _currentColor : targetColor < _currentColor)
        {
            _currentColor += change;
            spriteRenderer.color = Color.HSVToRGB(H, S, _currentColor);
            if(change > 0 ? targetColor <= _currentColor : targetColor >= _currentColor) 
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
[Serializable]
 public class OblectInformation
{
    public SpriteRenderer spriteRenderer { private set; get; }
    public float StartColor { private set; get; }
    private BoxCollider2D _boxCollider;

    public OblectInformation(SpriteRenderer spriteRenderer, BoxCollider2D boxCollider2D)
    {
        this.spriteRenderer = spriteRenderer;
        _boxCollider = boxCollider2D;
        Color.RGBToHSV(this.spriteRenderer.color, out float H, out float S, out float V);
        StartColor = (float)Math.Round(V, 2);
    }
    public void ColliderEnable(bool state)
    {
        _boxCollider.enabled = state;
    }
} 
