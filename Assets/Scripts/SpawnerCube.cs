using System.Collections;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _delay;

    private IEnumerator Start()
    {
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForSeconds(_delay);
            var position = (Vector2)transform.position + Vector2.right * Random.Range(-1, 1);
            var rotation = Quaternion.Euler(Vector3.forward * Random.Range(70, 160));
            Instantiate(_cube, position, rotation);
        }
    }
}
