using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;
    private void Start()
    {
        _hintObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        _hintObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _hintObject.SetActive(false);
    }
}
