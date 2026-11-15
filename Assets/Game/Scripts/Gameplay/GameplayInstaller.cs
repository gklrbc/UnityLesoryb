using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.Views;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller<GameplayInstaller>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CharacterView _characterPrefab;

    public override void InstallBindings()
    {
        BindCharacterView();
        BindCharacterMover();
        BindCharacterAxeUse();
    }

    private void BindCharacterView()
    {
        Container
            .Bind<CharacterView>()
            .FromComponentInNewPrefab(_characterPrefab)
            .UnderTransform(_spawnPoint)
            .AsSingle()
            .NonLazy();
    }

    private void BindCharacterMover()
    {
        Container
            .BindInterfacesAndSelfTo<CharacterMover>()
            .AsSingle()
            .NonLazy();
    }

    private void BindCharacterAxeUse()
    {
        Container
            .BindInterfacesAndSelfTo<CharacterAxeUse>()
            .AsSingle()
            .NonLazy();
    }
}