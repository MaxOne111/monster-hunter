using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private AmmoOnScene _Ammo;
    public override void InstallBindings()
    {
        Container.Bind<AmmoOnScene>().FromInstance(_Ammo).AsSingle().NonLazy();
    }
}