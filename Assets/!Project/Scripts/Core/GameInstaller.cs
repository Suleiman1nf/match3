using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Restart;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Core.UI;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using _Project.Scripts.Gameplay.GameGrid.Commands;
using _Project.Scripts.Gameplay.GameGrid.World;
using _Project.Scripts.Gameplay.GameLevel;
using _Project.Scripts.Gameplay.InputManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private UIService.Settings _uiSettings;
        [SerializeField] private AudioService.Settings _audioSettings;
        [SerializeField] private CubeFactory.Settings _cubeFactorySettings;
        [SerializeField] private WorldGridService.Settings _worldGridSettings;
        [SerializeField] private BackgroundImageService.Settings _backgroundImageServiceSettings;
        [SerializeField] private BalloonsSpawnService.Settings _balloonsSpawnServiceSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle().NonLazy();
            SaveServiceInstaller.Install(Container);
            UIServiceInstaller.Install(Container,_uiSettings);
            AudioServiceInstaller.Install(Container, _audioSettings);
            BackgroundImageServiceInstaller.Install(Container, _backgroundImageServiceSettings);
            BalloonsSpawnServiceInstaller.Install(Container, _balloonsSpawnServiceSettings);
            WorldGridServiceInstaller.Install(Container, _worldGridSettings);
            CubeFactoryInstaller.Install(Container, _cubeFactorySettings);
            GameLogicInstaller.Install(Container);
            SwipeInputServiceInstaller.Install(Container);
            FallServiceInstaller.Install(Container);
            CubeGridMoveServiceInstaller.Install(Container);
            SwapServiceInstaller.Install(Container);
            MatchServiceInstaller.Install(Container);
            CommandFactoryInstaller.Install(Container);
            RestartServiceInstaller.Install(Container);
            LevelLoadServiceInstaller.Install(Container);
        }
    }
}