using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core.Restart
{
    public class RestartService
    {
        private const string GameSceneName = "Game";
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}