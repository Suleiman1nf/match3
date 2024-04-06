using UnityEngine;

namespace _Project.Scripts.Core.Save
{
    public class PlayerPrefsSave<T> : IGameSaveMethod<T> where T : class, new()
    {
        private const string SavePath = "game_save";

        public void Save(T gameSave)
        {
            PlayerPrefs.SetString(SavePath, JsonUtility.ToJson(gameSave));
        }

        public T Load()
        {
            T gameSave;
            if (PlayerPrefs.HasKey(SavePath))
            {
                gameSave = JsonUtility.FromJson<T>(PlayerPrefs.GetString(SavePath));
            }
            else
            {
                gameSave = new T();
            }
            return gameSave;
        }
    }
}