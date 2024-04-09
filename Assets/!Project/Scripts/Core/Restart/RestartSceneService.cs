using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core.Restart
{
    public class RestartSceneService
    {
        private const string GameSceneName = "Game";
        
        public void Restart()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}