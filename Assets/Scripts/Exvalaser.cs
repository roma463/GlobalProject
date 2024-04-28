using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exvalaser : MonoBehaviour
{
    [SerializeField] private int numberOfBars = 64; // Количество полосок в эквалайзере
    [SerializeField] private float sensitivity = 100.0f; // Чувствительность эквалайзера

    [SerializeField] private float _offset;
    [SerializeField] private GameObject _prefabCubeElement;
    [SerializeField] private float _speedMoveLines = .5f;
    [SerializeField] private int _countVisibleLine = 3;
    private float[] audioData; // Массив для хранения аудио данных
    private GameObject[] _listOfject;

    void Start()
    {
        audioData = new float[numberOfBars]; // Инициализация массива аудио данных
        _listOfject = new GameObject[numberOfBars];
        for (int i = 0; i < _countVisibleLine; i++)
        {
            _listOfject[i] = Instantiate(_prefabCubeElement, transform.position + (Vector3.right * i * _offset), transform.rotation);
            _listOfject[i].transform.parent = transform;
        }
    }

    void Update()
    {
        // Получаем аудио данные из источника звука
        AudioListener.GetSpectrumData(audioData, 0, FFTWindow.Rectangular);

        // Отрисовываем полоски эквалайзера
        for (int i = 0; i < _countVisibleLine; i++)
        {
            float barHeight = audioData[i] * sensitivity;
            Vector3 scale = new Vector3(_listOfject[i].transform.localScale.x, barHeight, 1);

            _listOfject[i].transform.localScale = Vector3.MoveTowards(_listOfject[i].transform.localScale, scale, _speedMoveLines * Time.deltaTime);
        }
    }
}
