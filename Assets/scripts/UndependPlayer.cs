using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndependPlayer : Undepend
{
    [SerializeField] private Transform _pointUp;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private Vector2 _sizeBox;
    private void Update()
    {
       if(OverlapBox(_pointUp.position) || OverlapBox(_pointDown.position))
        {
            ResetForceOnTrigger();
        }
    }
    private bool OverlapBox(Vector2 point)
    {
        return Physics2D.OverlapBox(point, _sizeBox, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_pointUp.position, _sizeBox);
        Gizmos.DrawWireCube(_pointDown.position, _sizeBox);
        
    }
}
