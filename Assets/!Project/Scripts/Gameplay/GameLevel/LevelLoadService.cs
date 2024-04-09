using _Project.Scripts.Core;
using _Project.Scripts.Core.Restart;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Background;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.World;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelLoadService
    {
        private readonly RestartSceneService _restartSceneService;
        private readonly SaveService _saveService;
        private readonly BackgroundImageService _backgroundImageService;
        private readonly BalloonsSpawnService _balloonsSpawnService;
        private readonly WorldGridService _worldGridService;
        private readonly GameSettings _gameSettings;

        public GridModel InitialGrid { get; private set; }

        public LevelLoadService(
            GameSettings gameSettings, 
            SaveService saveService, 
            RestartSceneService restartSceneService,
            BackgroundImageService backgroundImageService, 
            BalloonsSpawnService balloonsSpawnService, 
            WorldGridService worldGridService)
        {
            _gameSettings = gameSettings;
            _saveService = saveService;
            _backgroundImageService = backgroundImageService;
            _balloonsSpawnService = balloonsSpawnService;
            _worldGridService = worldGridService;
            _restartSceneService = restartSceneService;
        }

        public void LoadLevel()
        {
            LevelData currentLevel = _gameSettings.Levels[_saveService.GameSave.CurrentLevel % _gameSettings.Levels.Count];
            _backgroundImageService.SetBackground(currentLevel.Background);
            _balloonsSpawnService.StartSpawning();
            InitialGrid = LoadLastGrid(currentLevel);
            _worldGridService.Init(InitialGrid.SizeX, InitialGrid.SizeY);
        }

        public void RestartLevel()
        {
            EraseModifiedGrid();
            _saveService.Save();
            
            _restartSceneService.Restart();
        }

        public void LoadNextLevel()
        {
            _saveService.GameSave.CurrentLevel = (_saveService.GameSave.CurrentLevel + 1) % _gameSettings.Levels.Count;
            EraseModifiedGrid();
            _saveService.Save();
            
            _restartSceneService.Restart();
        }

        private void EraseModifiedGrid()
        {
            _saveService.GameSave.GridData = "";
        }

        private GridModel LoadLastGrid(LevelData levelData)
        {
            string lastGrid = _saveService.GameSave.GridData;
            if (!string.IsNullOrEmpty(lastGrid))
            {
                return GridParser.FromData(lastGrid);
            }
            else
            {
                return GridParser.FromData(levelData.GridFile.text);
            }
        }
    }
}