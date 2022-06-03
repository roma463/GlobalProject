using System.Collections;
using UnityEngine;

public class InvisibleBlock : MonoBehaviour, Action
{
    [SerializeField] private bool _startActive;
    [Range(0, 1)]
    [SerializeField] private float blockout;
    [Range(0, 1)]
    [SerializeField] private float _changedInFrame;
    private SpriteRenderer[] _getCheldrensSpriteSenderer;
    private BoxCollider2D[] _getCheldrensBoxCollider;

    private void Start()
    {
        _getCheldrensSpriteSenderer = GetComponentsInChildren<SpriteRenderer>();
        _getCheldrensBoxCollider = GetComponentsInChildren<BoxCollider2D>();
        ActiveThisObject(_startActive);
    }
    public void launch()
    {
        ActiveThisObject(!_getCheldrensBoxCollider[0].enabled);
    }
    private void ActiveThisObject(bool stateActive)
    {
                StopAllCoroutines();
        int VectorDirectionColor = stateActive ? 1 : -1;
        //if (stateActive == true)
        //{
        //    for (int i = 0; i < _getCheldrensSpriteSenderer.Length; i++)
        //    {
        //        _getCheldrensBoxCollider[i].enabled = true;
        //        Color.RGBToHSV(_getCheldrensSpriteSenderer[i].color, out float H, out float S, out float V);
        //        Color Targer = Color.HSVToRGB(H, S, V + blockout);
        //        StartCoroutine(ColorByClick(_getCheldrensSpriteSenderer[i], Targer, _changedInFrame));
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < _getCheldrensSpriteSenderer.Length; i++)
        //    {
        //        _getCheldrensBoxCollider[i].enabled = false;
        //        Color.RGBToHSV(_getCheldrensSpriteSenderer[i].color, out float H, out float S, out float V);
        //        Color Targer = Color.HSVToRGB(H, S, V - blockout);
        //        StartCoroutine(ColorByClick(_getCheldrensSpriteSenderer[i], Targer, -_changedInFrame));
        //    }
        //}
        for (int i = 0; i < _getCheldrensSpriteSenderer.Length; i++)
        {
            _getCheldrensBoxCollider[i].enabled = stateActive;
            Color.RGBToHSV(_getCheldrensSpriteSenderer[i].color, out float H, out float S, out float V);
            Color Targer = Color.HSVToRGB(H, S, V + blockout * VectorDirectionColor);
            StartCoroutine(ColorByClick(_getCheldrensSpriteSenderer[i], Targer, _changedInFrame * VectorDirectionColor));
        }
    }
    private IEnumerator ColorByClick(SpriteRenderer spriteRenderer, Color TargetColor, float change)
    {
        Color.RGBToHSV(spriteRenderer.color, out float H, out float S, out float V);
        while (spriteRenderer.color != TargetColor)
        {
            V += change;
            if(V > 1 || V < 0) 
            {
                yield break;
            }
            print(V);
            spriteRenderer.color = Color.HSVToRGB(H, S, V);
            yield return new WaitForFixedUpdate();
        }
    }
}
