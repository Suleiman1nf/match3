using System;
using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Core.UI;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.GameLevel;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameBoot : MonoBehaviour
    {
        private SaveService _saveService;
        private AudioService _audioService;
        private LevelLoadService _levelLoadService;
        private GameLogic _gameLogic;
        private UIService _uiService;

        [Inject]
        public void Construct(
            GameSettings gameSettings,
            SaveService saveService, 
            AudioService audioService, 
            GameLogic gameLogic,
            LevelLoadService levelLoadService,            
            UIService uiService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _gameLogic = gameLogic;
            _uiService = uiService;
            _levelLoadService = levelLoadService;
        }

        public void Start()
        {
            Application.targetFrameRate = 60;
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _uiService.Init();
            _uiService.ShowGamePanel();
            _levelLoadService.LoadLevel();
            _gameLogic.Start();
        }

        private void OnApplicationQuit()
        {
            _saveService.Save();
        }
    }
}
