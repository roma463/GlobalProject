using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuLoader : IDisposable, IInitializable
{
    public void Dispose()
    {
        Debug.Log("Dispose");
    }

    public void Initialize()
    {
        Debug.Log("Initialize");
        LoadedMenu();
    }

    private void LoadedMenu()
    {
        SceneManager.LoadScene(1);
    }
}
