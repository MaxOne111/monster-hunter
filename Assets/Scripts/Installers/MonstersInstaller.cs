using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonstersInstaller : MonoInstaller
{
    [SerializeField] private MonstersPool _Monsters;

    public override void InstallBindings()
    {
        Container.Bind<MonstersPool>().FromInstance(_Monsters).AsSingle().NonLazy();
    }
}
