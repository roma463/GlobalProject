using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj;
        if (collision.transform.parent != null)
            collisionObj = collision.transform.parent.gameObject;
        else
            collisionObj = collision.gameObject;
        if (collisionObj.TryGetComponent(out Rigidbody2D rigidbody))
        {
            var scale = collisionObj.transform.localScale;
            scale.y *= -1;
            scale.x *= -1;
            collisionObj.transform.localScale = scale;
            rigidbody.gravityScale *= -1;
            if(collisionObj.gameObject == Teleport.GlobaTP.gameObject)
            {
                Teleport.GlobaTP.UpdateDataGraviryScale(rigidbody);
            }
        }
    }
}
