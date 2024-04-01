using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singlton : MonoBehaviour
{
    public virtual void Awake()
    {
        InitializeSingleton();
    }

    public abstract void InitializeSingleton();
}
