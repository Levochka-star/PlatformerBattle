using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnerHealer : Spawner<Healer>
    {
        [SerializeField] private Transform _pointsTarget;

        private Healer _healer;

        private void Start()
        {
            Work();
        }

        private void Work()
        {
            _healer = Spawn();
            _healer.transform.position = GenerateRandomPoint();

            _healer.Destroed += OnDestroed;
        }

        private void OnDestroed()
        {
            _healer.Destroed -= OnDestroed;

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