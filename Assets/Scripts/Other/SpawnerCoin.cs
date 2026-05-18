using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnerCoin : Spawner<Coin>
    {
        [SerializeField] private Transform _pointsTarget;

        private Coin _coin;

        private void Start()
        {
            Work();
        }

        private void Work()
        {
            _coin = Spawn();
            _coin.transform.position = GenerateRandomPoint();
        }

        protected override void ReturnToPool(Coin obj)
        {
            base.ReturnToPool(obj);

            Work();
        }

        private Vector2 GenerateRandomPoint()
        {
            int randomChild = Random.Range(0, _pointsTarget.childCount);

            Vector2 nextPoint = _pointsTarget.GetChild(randomChild).position;

            return nextPoint;
        }
    }
}