using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class SwapServiceInstaller : Installer<SwapServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SwapService>().FromNew().AsSingle();
        }
    }
}