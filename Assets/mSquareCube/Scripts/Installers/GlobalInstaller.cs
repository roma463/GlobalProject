using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveGame>().AsSingle().NonLazy();
        //Container.BindInterfacesAndSelfTo<MenuLoader>().AsSingle().NonLazy();
    }
}
