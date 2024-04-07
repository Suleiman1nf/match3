using Zenject;

namespace _Project.Scripts.Gameplay.InputManagement
{
    public class SwipeInputServiceInstaller : Installer<SwipeInputServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SwipeInputService>().FromNew().AsSingle();
        }
    }
}