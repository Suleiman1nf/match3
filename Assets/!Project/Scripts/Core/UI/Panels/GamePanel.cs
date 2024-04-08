using _Project.Scripts.Core.Restart;
using deVoid.UIFramework;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.UI.Panels
{
    public class GamePanel : APanelController
    {
        private RestartService _restartService;

        [Inject]
        private void Construct(RestartService restartService)
        {
            _restartService = restartService;
        }
        
        public void Next()
        {
            Debug.Log("Next");
        }

        public void Restart()
        {
            Debug.Log("Restart");
            _restartService.RestartLevel();
        }
    }
}