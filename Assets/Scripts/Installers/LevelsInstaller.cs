using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelsInstaller : MonoInstaller
{
    [SerializeField] private GameLevels _Levels;
    public override void InstallBindings()
    {
        Container.Bind<GameLevels>().FromInstance(_Levels).AsSingle().NonLazy();
    }
}
