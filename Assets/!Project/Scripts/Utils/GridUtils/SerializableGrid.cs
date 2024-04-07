using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Utils.GridUtils
{
    [Serializable]
    public class SerializableGrid<T> : ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] private List<ArrayElement<T>> _serializableList;
        [SerializeField, HideInInspector] private int _sizeX;
        [SerializeField, HideInInspector] private int _sizeY;
        private T[,] _array2D;

        public SerializableGrid(T[,] array2D)
        {
            _array2D = array2D;
            _sizeX = _array2D.GetLength(0);
            _sizeY = _array2D.GetLength(1);
        }
        
        public T Get(int i, int j)
        {
            return _array2D[i, j];
        }

        public void Set(int i, int j, T value)
        {
            _array2D[i, j] = value;
        }

        public void OnBeforeSerialize()
        {
            _serializableList = new List<ArrayElement<T>>();
            for (int i = 0; i < _array2D.GetLength(0); i++)
            {
                for (int j = 0; j < _array2D.GetLength(1); j++)
                {
                    _serializableList.Add(new ArrayElement<T>(i, j, _array2D[i, j]));
                }
            }
        }
        
        public void OnAfterDeserialize()
        {
            _array2D = new T[_sizeX, _sizeY];
            foreach(var element in _serializableList)
            {
                _array2D[element.Index0, element.Index1] = element.Element;
            }
        }
    }
}