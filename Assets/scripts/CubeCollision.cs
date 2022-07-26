using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private Undepend _undepend;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _undepend.ResetForceOnTrigger();
    }
}
