using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubes : MonoBehaviour
{
    [SerializeField] private Transform _Point;
    //private Transform[,] _matrics;

    private void Start()
    {
        transform.eulerAngles = Vector3.right * 90;
    }

    private void Update()
    {
        var distance = Vector2.Distance((Vector2)_Point.position, (Vector2)transform.position);
            distance = Mathf.Clamp(distance, 0, 7f);
            var rotate = distance * 90 - 90;
            rotate = Mathf.Clamp(rotate, 0, 90);
            transform.eulerAngles = Vector3.right * rotate;
        if(distance < 10)
        {
        }

    }
}
