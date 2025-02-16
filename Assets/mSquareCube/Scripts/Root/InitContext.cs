using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitContext : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.BindInstance(_levelList).AsSingle();
        //Container.BindInterfacesAndSelfTo<SaveGame>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<MenuLoader>().AsSingle().NonLazy();
    }
}
