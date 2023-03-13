using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _Player;
    public override void InstallBindings()
    {
        PlayerShootBind();
        PlayerMovementBind();
        PlayerWeaponBind();
        PlayerPetsBind();
        PlayerAlliesBind();
    }

    private void PlayerShootBind()
    {
        Container
            .Bind<PlayerShoot>()
            .FromInstance(_Player.GetComponent<PlayerShoot>())
            .AsSingle()
            .NonLazy();
    }

    private void PlayerMovementBind()
    {
        Container.Bind<PlayerMovement>()
            .FromInstance(_Player.GetComponent<PlayerMovement>())
            .AsSingle()
            .NonLazy();
    }

    private void PlayerWeaponBind()
    {
        Container
            .Bind<PlayerWeapon>()
            .FromInstance(_Player.GetComponent<PlayerWeapon>())
            .AsSingle()
            .NonLazy();
    }

    private void PlayerPetsBind()
    {
        Container
            .Bind<PlayerPets>()
            .FromInstance(_Player.GetComponent<PlayerPets>())
            .AsSingle()
            .NonLazy();
    }

    private void PlayerAlliesBind()
    {
        Container
            .Bind<PlayerAllies>()
            .FromInstance(_Player.GetComponent<PlayerAllies>())
            .AsSingle()
            .NonLazy();
    }
}