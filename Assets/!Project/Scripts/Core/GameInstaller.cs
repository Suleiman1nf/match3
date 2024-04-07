using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.GameLevel;
using _Project.Scripts.Gameplay.GridPlacement;
using _Project.Scripts.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle().NonLazy();
            SaveServiceInstaller.Install(Container);
            AudioServiceInstaller.Install(Container);
            BackgroundServiceInstaller.Install(Container);
            BalloonsSpawnServiceInstaller.Install(Container);
            GridPlacementServiceInstaller.Install(Container);
            CubeFactoryInstaller.Install(Container);
            LevelServiceInstaller.Install(Container);
            GridMovementServiceInstaller.Install(Container);
        }
    }
}