using Zenject;

namespace _Project.Scripts.Core.Audio
{
    public class AudioServiceInstaller : Installer<AudioServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioService>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}