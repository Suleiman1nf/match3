﻿using System;
using _Project.Scripts.Core.Audio;

namespace _Project.Scripts.Core.Save
{
    [Serializable]
    public class GameSave
    {
        public AudioSave AudioSave = new AudioSave();
        public string GridData = "";
        public int CurrentLevel = 0;
    }
}