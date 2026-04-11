using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Coin : MonoBehaviour, IPoolable<Coin>
    {
        public event Action<Coin> ReadyToDestroy;
        public event Action Destroed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>())
            {
                Destroed?.Invoke();
                ReadyToDestroy?.Invoke(this);
            }
        }
    }
}