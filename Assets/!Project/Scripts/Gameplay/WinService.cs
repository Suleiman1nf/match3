using System;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameLevel;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class WinService : IInitializable, IDisposable
    {
        private readonly CubeFactory _cubeFactory;
        private readonly LevelLoadService _levelLoadService;

        public WinService(CubeFactory cubeFactory, LevelLoadService levelLoadService)
        {
            _cubeFactory = cubeFactory;
            _levelLoadService = levelLoadService;
        }

        public void Initialize()
        {
            _cubeFactory.OnDestroyCube += OnCubeDestroyed;
        }

        public void Dispose()
        {
            _cubeFactory.OnDestroyCube -= OnCubeDestroyed;
        }

        private void OnCubeDestroyed(CubeController cubeController)
        {
            if (_cubeFactory.CreatedCubesCount <= 0)
            {
                _levelLoadService.LoadNextLevel();
            }
        }
    }
}