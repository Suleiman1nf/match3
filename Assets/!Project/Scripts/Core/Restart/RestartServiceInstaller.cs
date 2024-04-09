using Zenject;

namespace _Project.Scripts.Core.Restart
{
    public class RestartServiceInstaller : Installer<RestartServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<RestartSceneService>().FromNew().AsSingle();
        }
    }
}