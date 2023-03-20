using UnityEngine;
using Zenject;

public class RewardedAdInstaller : MonoInstaller
{
    [SerializeField] private MobAdsRewarded _Mob_Ad;
    [SerializeField] private AdReward _Ad_Reward;
    public override void InstallBindings()
    {
        Container.Bind<MobAdsRewarded>().FromInstance(_Mob_Ad).AsSingle().NonLazy();
        Container.Bind<AdReward>().FromInstance(_Ad_Reward).AsSingle().NonLazy();
    }
}