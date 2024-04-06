using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class Bounds
    {
        public readonly Vector2 LeftTop;
        public readonly Vector2 RightTop;
        public readonly Vector2 LeftBottom;
        public readonly Vector2 RightBottom;

        public Bounds(Vector2 leftTop, Vector2 rightTop, Vector2 leftBottom, Vector2 rightBottom)
        {
            LeftTop = leftTop;
            RightTop = rightTop;
            LeftBottom = leftBottom;
            RightBottom = rightBottom;
        }
    }
}