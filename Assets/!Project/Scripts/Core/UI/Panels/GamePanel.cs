using _Project.Scripts.Gameplay.GameLevel;
using deVoid.UIFramework;
using Zenject;

namespace _Project.Scripts.Core.UI.Panels
{
    public class GamePanel : APanelController
    {
        private LevelLoadService _levelLoadService;

        [Inject]
        private void Construct(LevelLoadService levelLoadService)
        {
            _levelLoadService = levelLoadService;
        }
        
        public void Next()
        {
            _levelLoadService.LoadNextLevel();
        }

        public void Restart()
        {
            _levelLoadService.RestartLevel();
        }
    }
}