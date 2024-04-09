using UnityEngine;

public class AnimationActivityObject : MonoBehaviour
{
    [SerializeField] private Sprite _enable, _disable;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Change(bool state)
    {
        if(state)
        {
            _spriteRenderer.sprite = _enable;
        }
        else
        {
            _spriteRenderer.sprite = _disable;
        }
    }
}
