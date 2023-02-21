using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableObject : MonoBehaviour
{
    [SerializeField] private GameObject _disableObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _disableObject.SetActive(false);
    }
}
