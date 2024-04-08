using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Core.UI;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.GameGrid;
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
        private WorldGridService _worldGridService;
        private UIService _uiService;

        [Inject]
        public void Construct(SaveService saveService, 
            AudioService audioService, 
            BackgroundImageService backgroundImageService, 
            BalloonsSpawnService balloonsSpawnService,
            LevelService levelService,
            WorldGridService worldGridService,
            UIService uiService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _backgroundImageService = backgroundImageService;
            _balloonsSpawnService = balloonsSpawnService;
            _levelService = levelService;
            _worldGridService = worldGridService;
            _uiService = uiService;
        }

        public void Start()
        {
            Application.targetFrameRate = 60;
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _backgroundImageService.SetBackground(_testBackground);
            _balloonsSpawnService.StartSpawning();
            _uiService.Init();
            _uiService.ShowGamePanel();
            GridModel model = GridParser.FromData(_level.text);
            _worldGridService.Init(model.SizeX, model.SizeY);
            _levelService.Load(model);
        }
    }
}
