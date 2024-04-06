using Zenject;

namespace _Project.Scripts.Core.Save
{
    public class SaveServiceInstaller : Installer<SaveServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameSaveMethod<GameSave>>().To<PlayerPrefsSave<GameSave>>().AsSingle();
            Container.Bind<SaveService>().FromNew().AsSingle();
        }
    }
}