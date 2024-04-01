
public class LanguageMenu : LanguageText
{
    public override void Start()
    {
        MenuSettings.Instance.LanguageUpdate += UpdateText;
        base.Start();
    }

    public override void UpdateText()
    {
        base.UpdateText();
    }
}
