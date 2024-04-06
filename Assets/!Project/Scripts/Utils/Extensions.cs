using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public static class Extensions
    {
        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}