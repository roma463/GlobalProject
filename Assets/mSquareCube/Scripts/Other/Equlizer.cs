using UnityEngine;

public class Equlizer : MonoBehaviour
{
    private static Equlizer _instance;

    [SerializeField] private int numberOfBars = 64;
    [SerializeField] private float sensitivity = 100.0f;

    [SerializeField] private float _offset;
    [SerializeField] private GameObject _prefabCubeElement;
    [SerializeField] private float _speedMoveLines = .5f;
    [SerializeField] private int _countVisibleLine = 3;
    [SerializeField] private bool _createIsRight = true;

    private float[] audioData;
    private GameObject[] _listOfject;
    private bool _isEnable;

    public static void Activate()
    {
        _instance.StartAnimation();
    }

    public void StartAnimation()
    {

        _isEnable = true;
        audioData = new float[numberOfBars];
        _listOfject = new GameObject[numberOfBars];
        for (int i = 0; i < _countVisibleLine; i++)
        {
            var directionCreate = _createIsRight ? transform.right : -transform.right;
            _listOfject[i] = Instantiate(_prefabCubeElement, transform.position + (directionCreate * i * _offset), transform.rotation);
            _listOfject[i].transform.parent = transform;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (!_isEnable)
            return;

        AudioListener.GetSpectrumData(audioData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < _countVisibleLine; i++)
        {
            float barHeight = audioData[i] * sensitivity;
            Vector3 scale = new Vector3(_listOfject[i].transform.localScale.x, barHeight, 1);

            _listOfject[i].transform.localScale = Vector3.MoveTowards(_listOfject[i].transform.localScale, scale, _speedMoveLines * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (!_isEnable)
            return;

        for (int i = 0; i < _countVisibleLine; i++)
        {
            var directionCreate = _createIsRight ? transform.right : -transform.right;
            Gizmos.DrawCube(transform.position + (directionCreate * i * _offset), Vector2.one * .1f);
        }
    }
}
