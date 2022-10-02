using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    [SerializeField] private string[] _text;
    [SerializeField] private float _delayPrint;
    [SerializeField] private float _pauseText;
    private Text _subtitlesText;

    private void Start ()
    {
        _subtitlesText = GetComponent<Text>();
        StartSubtitles(_text);
    }

    public void StartSubtitles (string[] texts)
    {
        StartCoroutine(Conclusion(texts));
    }

    IEnumerator Conclusion (string[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            foreach (char ch in texts[i])
            {
                yield return new WaitForSeconds(_delayPrint);
                _subtitlesText.text += ch;
            }

            yield return new WaitForSeconds(_pauseText);
            _subtitlesText.text = "";
        }

        yield break;
    }
}
