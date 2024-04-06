namespace _Project.Scripts.Core.Save
{
    public class SaveService
    {
        private readonly IGameSaveMethod<GameSave> _gameSaveMethod;
        public GameSave GameSave { get; private set; }

        public SaveService(IGameSaveMethod<GameSave> gameSaveMethod)
        {
            _gameSaveMethod = gameSaveMethod;
        }

        public void Save()
        {
            _gameSaveMethod.Save(GameSave);
        }

        public void Load()
        {
            GameSave = _gameSaveMethod.Load();
        }
    }
}