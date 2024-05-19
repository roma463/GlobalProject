using UnityEngine;


public class HintCollisiion : MonoBehaviour
{
    [SerializeField] private Hint _hintText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hintText.AnimationShow();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _hintText.AnimationHide();
    }
}
