using System;

namespace _Project.Scripts.Utils.GridUtils
{
    [Serializable]
    public class ArrayElement<T>
    {
        public int Index0;
        public int Index1;
        public T Element;
        public ArrayElement(int idx0, int idx1, T element)
        {
            Index0 = idx0;
            Index1 = idx1;
            Element = element;
        }
    }
}