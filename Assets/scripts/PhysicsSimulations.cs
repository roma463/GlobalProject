using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSimulations : MonoBehaviour
{
    [SerializeField] private float _partTime;
    private int i = 0;
    private Scene _bullets;
    private PhysicsScene2D _physicsBullet;
    private PhysicsScene2D _physicsMain;
    [SerializeField] private LayerMask layerMask;
    private void Start()
    {
        CreateSceneParameters parametersScene = new CreateSceneParameters();
        parametersScene.localPhysicsMode = LocalPhysicsMode.Physics2D;
        _physicsMain = SceneManager.GetActiveScene().GetPhysicsScene2D();
        _bullets = SceneManager.CreateScene("Bullets_scene",parametersScene);
        _physicsBullet = _bullets.GetPhysicsScene2D();
    }
    private void FixedUpdate()
    {
        i++;
        if (i == _partTime)
        {
            i = 0;
            _physicsMain.Simulate(Time.fixedDeltaTime * _partTime);
        }
        _physicsBullet.Simulate(Time.fixedDeltaTime);
    }
    public void AddObjectInScene(GameObject obj)
    {
        SceneManager.MoveGameObjectToScene(obj, _bullets);
    }
}
