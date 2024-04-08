using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using _Project.Scripts.Gameplay.GameGrid.World;
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
        private BackgroundImageService _backgroundImageService;
        private BalloonsSpawnService _balloonsSpawnService;
        private LevelService _levelService;
        private GridService _gridService;
        private WorldGridService _worldGridService;

        [Inject]
        public void Construct(SaveService saveService, 
            AudioService audioService, 
            BackgroundImageService backgroundImageService, 
            BalloonsSpawnService balloonsSpawnService,
            LevelService levelService,
            GridService gridService,
            WorldGridService worldGridService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _backgroundImageService = backgroundImageService;
            _balloonsSpawnService = balloonsSpawnService;
            _levelService = levelService;
            _gridService = gridService;
            _worldGridService = worldGridService;
        }

        public void Start()
        {
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _backgroundImageService.SetBackground(_testBackground);
            _balloonsSpawnService.StartSpawning();
            GridModel model = GridParser.FromData(_level.text);
            _gridService.Init(model);
            _worldGridService.Init(model.SizeX, model.SizeY);
            _levelService.Load();
        }
    }
}
