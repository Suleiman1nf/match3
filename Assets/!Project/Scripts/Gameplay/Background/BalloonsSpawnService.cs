using System;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using UnityEngine;
using Bounds = _Project.Scripts.Utils.Bounds;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Background
{
    public class BalloonsSpawnService
    {
        private readonly Settings _settings;

        private Bounds _bounds;

        public BalloonsSpawnService(Settings settings)
        {
            _settings = settings;
        }

        private Bounds Bounds => _bounds ??= CalculateBounds();

        public void StartSpawning()
        {
            for (int i = 0; i < _settings.MaxBalloonsCount; i++)
            {
                Spawn();
            }
        }
        
        private void Spawn()
        {
            Balloon balloon = GameObject.Instantiate(_settings.BalloonsPrefabs.GetRandomElement(), _settings.BalloonsContainer);
            PlaceBalloon(balloon);
        }

        private void PlaceBalloon(Balloon balloon)
        {
            bool spawnOnLeftSide = Random.Range(0, 2) == 1;
            int direction = spawnOnLeftSide ? 1 : -1;
            float posX = spawnOnLeftSide ? Bounds.LeftBottom.x : Bounds.RightBottom.x;  
            float posY = Random.Range(Bounds.LeftBottom.y, Bounds.LeftTop.y);
            Vector2 position = new Vector2(posX, posY);
            float speed = direction * Random.Range(_settings.MinSpeed, _settings.MaxSpeed);
            balloon.transform.localScale = Vector3.one * Random.Range(_settings.MinSize, _settings.MaxSize);
            balloon.Init(speed, position, Bounds, OnOutBounds);
        }

        private void OnOutBounds(Balloon balloon)
        {
            PlaceBalloon(balloon);
        }

        private Bounds CalculateBounds()
        {
            var bounds = _settings.SpawnVisualBounds.bounds;
            Vector3 rightTop = bounds.max;
            Vector3 leftBottom = bounds.min;
            Vector2 leftTop = new Vector2(leftBottom.x, rightTop.y);
            Vector2 rightBottom = new Vector2(rightTop.x, leftBottom.y);
            return new Bounds(leftTop, rightTop, leftBottom, rightBottom);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int MaxBalloonsCount { get; private set; }
            [field: SerializeField] public List<Balloon> BalloonsPrefabs { get; private set; }
            [field: SerializeField] public Transform BalloonsContainer { get; private set; }
            [field: SerializeField] public SpriteRenderer SpawnVisualBounds { get; private set; }
            [field: SerializeField] public float MinSpeed { get; private set; }
            [field: SerializeField] public float MaxSpeed { get; private set; }
            [field: SerializeField] public float MinSize { get; private set; }
            [field: SerializeField] public float MaxSize { get; private set; }
        }
    }
}