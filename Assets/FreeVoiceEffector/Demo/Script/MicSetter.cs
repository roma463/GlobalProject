using UnityEngine;
using UnityEngine.UI;
namespace FreeVoiceEffector
{


public class MicSetter : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private void Start()
    {   
        var Mic = RealTimeMic.Instance;
        toggle = GetComponent<Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(Mic.SetMic);
    }
}
}