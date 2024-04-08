using System;
using deVoid.UIFramework;
using UnityEngine;

namespace _Project.Scripts.Core.UI
{
    public class UIService
    {
        private Settings _settings;

        private UIFrame _uiFrame;

        public UIService(Settings settings)
        {
            _settings = settings;
        }

        public void Init()
        {
            _uiFrame = _settings.UISettings.CreateUIInstance();
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