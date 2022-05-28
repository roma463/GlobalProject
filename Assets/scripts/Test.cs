using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private void Start()
    {
        _renderer.sortingLayerID = 3;
    }
}
