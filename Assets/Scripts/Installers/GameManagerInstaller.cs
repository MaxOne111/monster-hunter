using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private AmmoOnScene _Ammo;
    [SerializeField] private Purchase _Purchase;
    public override void InstallBindings()
    {
        Container.Bind<AmmoOnScene>().FromInstance(_Ammo).AsSingle().NonLazy();
        Container.Bind<Purchase>().FromInstance(_Purchase).AsSingle().NonLazy();
    }
}