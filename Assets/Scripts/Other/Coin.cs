using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Coin : MonoBehaviour, IPoolable<Coin>
    {
        public event Action<Coin> ReadyToDestroy;

        public void Destroy()
        {

            ReadyToDestroy?.Invoke(this);
        }
    }
}