using _Project.Scripts.Core.Restart;
using _Project.Scripts.Core.Save;
using deVoid.UIFramework;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.UI.Panels
{
    public class GamePanel : APanelController
    {
        private RestartService _restartService;
        private SaveService _saveService;

        [Inject]
        private void Construct(RestartService restartService, SaveService saveService)
        {
            _restartService = restartService;
            _saveService = saveService;
        }
        
        public void Next()
        {
            _saveService.GameSave.currentLevel++;
            _saveService.Save();
            Restart();
        }

        public void Restart()
        {
            _restartService.RestartLevel();
        }
    }
}