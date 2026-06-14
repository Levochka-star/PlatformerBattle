using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && !collision.isTrigger)
        {
            if (!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    public Enemy GetNearEnemy()
    {
        ClearDeadEnemy();

        float minDistans = float.MaxValue;

        Enemy nearEnemy = null;

        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == null)
            {
                continue;
            }

            float distans = (_enemies[i].transform.position - transform.position).sqrMagnitude;

            if (distans < minDistans)
            {
                minDistans = distans;

                nearEnemy = _enemies[i];
            }
        }

        return nearEnemy;
    }

    private void ClearDeadEnemy()
    {
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
        }
    }
}