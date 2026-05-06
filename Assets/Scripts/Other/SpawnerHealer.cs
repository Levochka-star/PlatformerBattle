using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnerHealer : Spawner<Healer>
    {
        [SerializeField] private Transform _pointsTarget;
        [SerializeField] private int _spawnDelay = 10;

        private Healer _healer;

        private Coroutine _waitSpawn;

        private void Start()
        {
            StartRespawn();
        }

        private void StartRespawn()
        {
            if (_waitSpawn == null)
            {
                _waitSpawn = StartCoroutine(WaitingRespawn(_spawnDelay));
            }
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

            StartRespawn();
        }

        private Vector2 GenerateRandomPoint()
        {
            int randomChild = Random.Range(0, _pointsTarget.childCount);

            Vector2 nextPoint = _pointsTarget.GetChild(randomChild).position;

            return nextPoint;
        }

        private IEnumerator WaitingRespawn (int  delay)
        {
            yield return new WaitForSeconds(delay);

            Work();
            StopCoroutine(_waitSpawn);

            _waitSpawn = null;
        }
    }
}