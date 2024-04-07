using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Movement;
using _Project.Scripts.Gameplay.GameGrid.Placement;
using _Project.Scripts.Gameplay.GameLevel;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameBoot : MonoBehaviour
    {
        [SerializeField] private GameObject _testBackground;
        [SerializeField] private GameObject _testObj;
        [SerializeField] private TextAsset _level;
        
        private SaveService _saveService;
        private AudioService _audioService;
        private BackgroundService _backgroundService;
        private BalloonsSpawnService _balloonsSpawnService;
        private LevelService _levelService;
        private GridService _gridService;
        private GridPlacementService _gridPlacementService;

        [Inject]
        public void Construct(SaveService saveService, 
            AudioService audioService, 
            BackgroundService backgroundService, 
            BalloonsSpawnService balloonsSpawnService,
            LevelService levelService,
            GridService gridService,
            GridPlacementService gridPlacementService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _backgroundService = backgroundService;
            _balloonsSpawnService = balloonsSpawnService;
            _levelService = levelService;
            _gridService = gridService;
            _gridPlacementService = gridPlacementService;
        }

        public void Start()
        {
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _backgroundService.SetBackground(_testBackground);
            _balloonsSpawnService.StartSpawning();
            GridModel model = GridParser.FromData(_level.text);
            _gridService.Init(model);
            _gridPlacementService.Init(model.SizeX, model.SizeY);
            _levelService.Load();
        }
    }
}
