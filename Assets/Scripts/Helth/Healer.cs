using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Healer : MonoBehaviour, IPoolable<Healer>
    {
        [SerializeField] private float _healPoint = 30f;

        public float HealPoint => _healPoint;

        public event Action<Healer> ReadyToDestroy;

        public void Destroy()
        {
            ReadyToDestroy?.Invoke(this);
        }
    }
}
