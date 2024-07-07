
public class LanguageMenu : LanguageText
{
    public override void Start()
    {
        if(MenuSettings.Instance != null)
            MenuSettings.Instance.LanguageUpdate += UpdateText;
        base.Start();
    }

    public override void UpdateText()
    {
        base.UpdateText();
    }
}
