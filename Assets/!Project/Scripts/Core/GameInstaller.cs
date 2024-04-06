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
        }
    }
}