using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _Player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerShoot>().FromInstance(_Player.GetComponent<PlayerShoot>()).AsSingle().NonLazy();
        Container.Bind<PlayerMovement>().FromInstance(_Player.GetComponent<PlayerMovement>()).AsSingle().NonLazy();
        Container.Bind<PlayerWeapon>().FromInstance(_Player.GetComponent<PlayerWeapon>()).AsSingle().NonLazy();
    }
}