using TMPro;
using UnityEngine;
using static SaveGame;

public class Episode : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _image;
    [SerializeField] private UnityEngine.UI.Button _imageButton;
    [SerializeField] private LanguageMenu _name;
    private ChoiseLevel _chooseLevel;
    private int _idStartLevel;

    public void Init(LevelsCotegory levelsCotegory, ChoiseLevel choiseLevel)
    {
        _image.sprite = levelsCotegory.GetSprite();
        _image.SetNativeSize();

        _name.Init(levelsCotegory.GetName(Language.rus), levelsCotegory.GetName(Language.eng));
        _chooseLevel = choiseLevel;
        _idStartLevel = levelsCotegory.Levels[0].buildIndex;
    }

    public void OnClick()
    {
        _chooseLevel.LaunchGame(_idStartLevel);
    }
}
