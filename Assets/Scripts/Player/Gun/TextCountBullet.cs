using TMPro;
using UnityEngine;

public class TextCountBullet : MonoBehaviour
{
    [SerializeField] private Teleport _teleport;
    [SerializeField] private TextMeshPro _textCountBullet;
    [SerializeField] private Color _zeroBullet;
    private Color _startColor;
    private Color _enabledColor;
    private bool _enabled = true;

    private void Start()
    {
        _textCountBullet.transform.parent = null;
        _startColor = _textCountBullet.color;
    }

    private void OnEnable()
    {
        GetComponent<PositionGun>().ChangedCountBullet += UpdateText;
        _teleport.Effects_E += ChangePosition;
    }

    private void OnDisable()
    {
        GetComponent<PositionGun>().ChangedCountBullet -= UpdateText;
        _teleport.Effects_E -= ChangePosition;
    }

    public void DisabledGun()
    {
        _enabled = false;
        _enabledColor = _textCountBullet.color;
        _textCountBullet.color = Color.clear;
    }

    public void EnabledGun()
    {
        _enabled = true;
        _textCountBullet.color = _enabledColor;
    }

    public void ChangePosition()
    {
        _textCountBullet.transform.position = transform.position;
    }

    public void UpdateText(int count)
    {
        _textCountBullet.text = count.ToString();
        if (!_enabled)
            return;
        if (count == 0)
        {
            _textCountBullet.color = _zeroBullet;
        }
        else
        {
            _textCountBullet.color = _startColor;
        }

    }
}
