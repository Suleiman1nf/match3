using System;
using deVoid.UIFramework;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.UI
{
    public class UIService
    {
        private Settings _settings;
        private DiContainer _diContainer;

        private UIFrame _uiFrame;

        public UIService(DiContainer diContainer, Settings settings)
        {
            _settings = settings;
            _diContainer = diContainer;
        }

        public void Init()
        {
            _uiFrame = _settings.UISettings.CreateUIInstance(_diContainer);
        }

        public void ShowGamePanel()
        {
            _uiFrame.ShowPanel(UIIds.GamePanel);
        }

        public void HideGamePanel()
        {
            _uiFrame.HidePanel(UIIds.GamePanel);
        }
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public UISettings UISettings { get; private set; }           
        }
    }
}