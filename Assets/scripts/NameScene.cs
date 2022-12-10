using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteAlways]
public class NameScene : MonoBehaviour
{
    [SerializeField] private Text _nameCurrentScene;
    
    [ExecuteAlways]
    private void Start()
    {
        _nameCurrentScene.text = SceneManager.GetActiveScene().name;
    }
}
