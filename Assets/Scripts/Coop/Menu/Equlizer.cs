using UnityEngine;

public class Equlizer : MonoBehaviour
{
    [SerializeField] private int numberOfBars = 64;
    [SerializeField] private float sensitivity = 100.0f;

    [SerializeField] private float _offset;
    [SerializeField] private GameObject _prefabCubeElement;
    [SerializeField] private float _speedMoveLines = .5f;
    [SerializeField] private int _countVisibleLine = 3;
    private float[] audioData;
    private GameObject[] _listOfject;

    private void Start()
    {
        audioData = new float[numberOfBars];
        _listOfject = new GameObject[numberOfBars];
        for (int i = 0; i < _countVisibleLine; i++)
        {
            _listOfject[i] = Instantiate(_prefabCubeElement, transform.position + (Vector3.right * i * _offset), transform.rotation);
            _listOfject[i].transform.parent = transform;
        }
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(audioData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < _countVisibleLine; i++)
        {
            float barHeight = audioData[i] * sensitivity;
            Vector3 scale = new Vector3(_listOfject[i].transform.localScale.x, barHeight, 1);

            _listOfject[i].transform.localScale = Vector3.MoveTowards(_listOfject[i].transform.localScale, scale, _speedMoveLines * Time.deltaTime);
        }
    }
}
