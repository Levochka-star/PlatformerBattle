using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _detectRadius = 3f;  
    [SerializeField] private LayerMask _enemyLayer;

    public float DetectRadius => _detectRadius;

    public Enemy GetNearEnemy()
    {
        Collider2D[] caughtColliders = Physics2D.OverlapCircleAll(transform.position, _detectRadius, _enemyLayer);

        float minDistans = float.MaxValue;

        Enemy nearEnemy = null;

        for (int i = 0; i < caughtColliders.Length; i++)
        {
            if (caughtColliders[i] == null || caughtColliders[i].isTrigger)
                continue;

            if (caughtColliders[i].TryGetComponent(out Enemy enemy))
            {
                float distans = (caughtColliders[i].transform.position - transform.position).sqrMagnitude;

                if (distans < minDistans)
                {
                    minDistans = distans;

                    nearEnemy = enemy;
                }
            }
        }

        return nearEnemy;
    }
}