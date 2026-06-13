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

        foreach (Enemy enemy in _enemies)
        {
            float distans = (enemy.transform.position - transform.position).sqrMagnitude;

            if (distans < minDistans)
            {
                minDistans = distans;

                nearEnemy = enemy;
            }
        }

        return nearEnemy;
    }

    private void ClearDeadEnemy()
    {
        foreach (Enemy enemy in _enemies)
        {
            if(enemy == null)
            {
                _enemies.Remove(enemy);
            }
        }
    }
}