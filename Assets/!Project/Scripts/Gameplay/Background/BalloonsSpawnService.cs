using System.Collections.Generic;
using _Project.Scripts.Utils;
using UnityEngine;
using Bounds = _Project.Scripts.Utils.Bounds;

namespace _Project.Scripts.Gameplay.Background
{
    public class BalloonsSpawnService : MonoBehaviour
    {
        [SerializeField] private int _maxBalloonsCount;
        [SerializeField] private List<Balloon> _balloonsPrefabs;
        [SerializeField] private Transform _balloonsContainer;
        [SerializeField] private SpriteRenderer _spawnVisualBounds;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;

        private Bounds _bounds;
        
        private Bounds Bounds => _bounds ??= CalculateBounds();

        public void StartSpawning()
        {
            for (int i = 0; i < _maxBalloonsCount; i++)
            {
                Spawn();
            }
        }
        
        private void Spawn()
        {
            Balloon balloon = Instantiate(_balloonsPrefabs.GetRandomElement(), _balloonsContainer);
            PlaceBalloon(balloon);
        }

        private void PlaceBalloon(Balloon balloon)
        {
            bool spawnOnLeftSide = Random.Range(0, 2) == 1;
            int direction = spawnOnLeftSide ? 1 : -1;
            float posX = spawnOnLeftSide ? Bounds.LeftBottom.x : Bounds.RightBottom.x;  
            float posY = Random.Range(Bounds.LeftBottom.y, Bounds.LeftTop.y);
            Vector2 position = new Vector2(posX, posY);
            float speed = direction * Random.Range(_minSpeed, _maxSpeed);
            balloon.transform.localScale = Vector3.one * Random.Range(_minSize, _maxSize);
            balloon.Init(speed, position, Bounds, OnOutBounds);
        }

        private void OnOutBounds(Balloon balloon)
        {
            PlaceBalloon(balloon);
        }

        private Bounds CalculateBounds()
        {
            var bounds = _spawnVisualBounds.bounds;
            Vector3 rightTop = bounds.max;
            Vector3 leftBottom = bounds.min;
            Vector2 leftTop = new Vector2(leftBottom.x, rightTop.y);
            Vector2 rightBottom = new Vector2(rightTop.x, leftBottom.y);
            return new Bounds(leftTop, rightTop, leftBottom, rightBottom);
        }
    }
}