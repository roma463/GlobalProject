 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speedMove;
    private Vector3 _offsetPos;
    private bool _isMove;

    public void StartMove ()
    {
        _isMove = true;
    }

    private void Update()
    {
        if (_isMove)
        {
            _offsetPos = new Vector3(_endPosition.position.x, _endPosition.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _endPosition.position, _speedMove);
 
        }

        if (Vector3.Distance(transform.position, _endPosition.position) == 0)
        {
            _isMove = false;
        }
    }
}
