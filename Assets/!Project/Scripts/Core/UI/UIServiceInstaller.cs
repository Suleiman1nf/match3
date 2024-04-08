using Zenject;

namespace _Project.Scripts.Core.UI
{
    public class UIServiceInstaller : Installer<UIService.Settings, UIServiceInstaller>
    {
        private UIService.Settings _settings;

        public UIServiceInstaller(UIService.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<UIService>().FromNew().AsSingle().WithArguments(_settings);
        }
    }
}