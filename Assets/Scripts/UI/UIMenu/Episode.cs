using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Episode : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _image;
    [SerializeField] private UnityEngine.UI.Button _imageButton;
    private ChoiseLevel _chooseLevel;
    private int _idStartLevel;

    public void Init(Sprite sprite, bool isOpen, ChoiseLevel choiseLevel, int idStartLevel)
    {
        _image.sprite = sprite;
        _chooseLevel = choiseLevel;
        _idStartLevel = idStartLevel;
    }

    public void OnClick()
    {
        _chooseLevel.LaunchGame(_idStartLevel);
    }
}
