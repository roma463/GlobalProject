using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndependBlock : Undepend
{
    [SerializeField] private UsePlayerObject _usePlayerObject;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_usePlayerObject.UseNow)
            base.OnCollisionEnter2D(collision);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_usePlayerObject.UseNow)
            base.OnTriggerEnter2D(collision);
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (!_usePlayerObject.UseNow)
            base.OnTriggerExit2D(collision);
    }
}
