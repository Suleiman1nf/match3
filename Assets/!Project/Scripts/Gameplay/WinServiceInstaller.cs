using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class WinServiceInstaller : Installer<WinServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WinService>().FromNew().AsSingle();
        }
    }
}