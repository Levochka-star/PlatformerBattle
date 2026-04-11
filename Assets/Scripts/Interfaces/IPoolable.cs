using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        public event Action<T> ReadyToDestroy;
    }
}
