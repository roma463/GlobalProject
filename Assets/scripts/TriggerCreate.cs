using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCreate : MonoBehaviour
{
    [SerializeField] private Animator _createRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    _createRoom.SetTrigger("Start");
    }
}
