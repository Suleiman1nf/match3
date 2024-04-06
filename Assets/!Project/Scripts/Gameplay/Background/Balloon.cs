using System;
using UnityEngine;
using Bounds = _Project.Scripts.Utils.Bounds;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Background
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Balloon : MonoBehaviour
    {
        private const float MinFrequency = 0.2f;
        private const float MaxFrequency = 1f;
        private const float MinAmplitude = 0.2f;
        private const float MaxAmplitude = 1f;

        private SpriteRenderer _spriteRenderer;
        private float _speed;
        private float _frequency;
        private float _amplitude;
        private Vector2 _startPosition;
        private Bounds _bounds;
        private Action<Balloon> _onOutBounds;
        private float _time;
        private bool _active;

        public void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(float speed, Vector2 position, Bounds bounds, Action<Balloon> onOutBounds)
        {
            _speed = speed;
            _bounds = bounds;
            _onOutBounds = onOutBounds;
            transform.position = position;
            _startPosition = transform.position;
            _time = 0;
            _frequency = Random.Range(MinFrequency, MaxFrequency);
            _amplitude = Random.Range(MinAmplitude, MaxAmplitude);
            _active = true;
        }

        public void Update()
        {
            if (!_active)
            {
                return;
            }

            _time += Time.deltaTime;
            Vector2 position = transform.position;
            position.x = _startPosition.x + _time * _speed;
            position.y = _startPosition.y + Mathf.Sin(_time * _frequency) * _amplitude;
            transform.position = position;
            
            if (_speed >= 0 && transform.position.x - _spriteRenderer.bounds.size.x / 2 > _bounds.RightTop.x)
            {
                _onOutBounds?.Invoke(this);
                return;
            }

            if (_speed < 0 && transform.position.x + _spriteRenderer.bounds.size.x / 2 < _bounds.LeftTop.x)
            {
                _onOutBounds?.Invoke(this);
                return;
            }
        }
    }
}