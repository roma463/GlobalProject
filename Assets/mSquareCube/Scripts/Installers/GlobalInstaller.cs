using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private LevelList _levelList; 
    public override void InstallBindings()
    {
        Container.BindInstance(_levelList).AsSingle();
        Container.BindInterfacesAndSelfTo<SaveGame>().AsSingle().NonLazy();
        //Container.BindInterfacesAndSelfTo<MenuLoader>().AsSingle().NonLazy();
    }
}
