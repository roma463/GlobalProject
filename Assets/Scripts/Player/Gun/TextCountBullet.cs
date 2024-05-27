using TMPro;
using UnityEngine;

public class TextCountBullet : MonoBehaviour
{
    [SerializeField] private Teleport _teleport;
    [SerializeField] private PositionGun _positionGun;
    [SerializeField] private TextMeshPro _textCountBullet;
    [SerializeField] private Color _zeroBullet;
    private Color _enabledColor;
    private bool _enabled = true;

    private void Awake()
    {
        _textCountBullet.transform.parent = null;
        _enabledColor = _textCountBullet.color;
    }

    private void OnEnable()
    {
        _positionGun.ChangedCountBullet += UpdateText;
        _teleport.Effects_E += ChangePosition;
    }

    private void OnDisable()
    {
        _positionGun.ChangedCountBullet -= UpdateText;
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
        if (_positionGun.GetInfinit())
            return;

        _textCountBullet.text = count.ToString();

        if (!_enabled)
            return;
        if (count == 0)
        {
            _textCountBullet.color = _zeroBullet;
        }
        else
        {
            _textCountBullet.color = _enabledColor;
        }

    }
}
