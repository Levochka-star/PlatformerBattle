using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Healer : MonoBehaviour, IPoolable<Healer>
    {
        [SerializeField] private float _heal = 30f;

        public event Action<Healer> ReadyToDestroy;

        public event Action Destroed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IHealable helable) && helable.TryHealling())
            {
                helable.Heal(_heal);

                Destroed?.Invoke();

                ReadyToDestroy?.Invoke(this);
            }
        }
    }
}
