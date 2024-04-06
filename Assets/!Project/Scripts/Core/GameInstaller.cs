using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
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
        }
    }
}