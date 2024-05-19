using UnityEngine;

public class DegubPause : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Break();
        }  
    }
}
