using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class testDrag : MonoBehaviour
{
    [SerializeField] private PlayerSpiteChoise[] _allPlayerSprites;
    [SerializeField] private Transform _imageDragPlayer;
    [SerializeField] private float _offsetInFrame = 1;
    [SerializeField] private Vector3 _openScale, _standartScale;
    private int lastChoisPlayer;
    private Camera _cameraMain;
    private void Start()
    {
        _cameraMain = Camera.main;
    }
    public void OnDrag()
    {
        var mousePosition = _cameraMain.ScreenToWorldPoint(Input.mousePosition);
        var CurrenMinimumDistance = Vector2.Distance(mousePosition, _allPlayerSprites[0].GetPosition());
        var mininumIndexInArray = 0;
        for (int i = 1; i < _allPlayerSprites.Length; i++)
        {
            if(Vector2.Distance(mousePosition, _allPlayerSprites[i].GetPosition()) < CurrenMinimumDistance)
            {
                mininumIndexInArray = i;
            }
        }
        if(lastChoisPlayer != mininumIndexInArray)
        {
           _allPlayerSprites[lastChoisPlayer].StartScaleChange(_standartScale);
           _allPlayerSprites[mininumIndexInArray].StartScaleChange(_openScale);
        }
        lastChoisPlayer = mininumIndexInArray;
        StopAllCoroutines();
        StartCoroutine(Direction(_allPlayerSprites[mininumIndexInArray].GetPosition()));

    }
    private IEnumerator Direction(Vector2 direction) 
    {
        while((Vector2)_imageDragPlayer.position != direction)
        {
            _imageDragPlayer.position = Vector2.MoveTowards(_imageDragPlayer.position, direction, _offsetInFrame * Time.deltaTime);
            yield return null;
        }
    }
}
[System.Serializable]
public class PlayerSpiteChoise 
{
    [SerializeField] private Transform _player;
    [SerializeField] private bool _isChoice;

    public Vector2 GetPosition()
    {
        return _player.position;
    }
    public bool GetIsChoice()
    {
        return _isChoice;
    }
    public void StartScaleChange(Vector3 scale)
    {
        while (true)
        {
            _player.localScale = Vector3.MoveTowards(_player.localScale, scale, .1f);
            if (_player.localScale == scale)
            {
                break;   
            }
        }
    }
    public IEnumerator changeScale(Vector3 ScaleTarget)
    {
        while (_player.localScale != ScaleTarget)
        {
            Debug.Log(_player.gameObject.name);
            _player.localScale = Vector3.MoveTowards(_player.localScale, ScaleTarget, .1f);
            yield return null;
        }
    }
}
