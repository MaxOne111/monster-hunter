using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private GameUI _Game_UI;
    public override void InstallBindings()
    {
        Container.Bind<GameUI>().FromInstance(_Game_UI).AsSingle().NonLazy();
    }
}
