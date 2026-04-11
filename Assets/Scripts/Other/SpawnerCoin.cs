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

            _coin.Destroed += OnDestroed;
        }

        private void OnDestroed()
        {
            _coin.Destroed -= OnDestroed;

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