using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class AbilityVampirism : MonoBehaviour
{
    [SerializeField] private float _pointPerSecond;
    [SerializeField] private float _workingTime = 6f;
    [SerializeField] private float _rechargeTime = 4f;

    [SerializeField] EnemyDetector _zoneAttack;

    private Health _health;
    private Coroutine _waitWorking;
    private Coroutine _waitRecharging;

    private bool _isReady;

    public event Action AbilityEnabled;
    public event Action<float> ChangedFillPoint;

    private void Awake()
    {
        _isReady = false;
        _health = GetComponent<Health>();

        StartingRecharch();
    }

    private void OnDisable()
    {
        if (_waitWorking != null)
            StopCoroutine(_waitWorking);

        if (_waitRecharging != null)
            StopCoroutine(_waitRecharging);
    }

    public void Work()
    {
        if (_isReady)
        {
            StartingVampirism();
        }
    }

    private void StartingVampirism()
    {
        if (_waitWorking != null)
        {
            StopCoroutine(_waitWorking);
        }
      
        _waitWorking = StartCoroutine(Working(_workingTime));
    }

    private void StartingRecharch()
    {
        if (_waitRecharging != null)
        {
            StopCoroutine(_waitRecharging);
        }

        _waitRecharging = StartCoroutine(Recharging(_rechargeTime));
    }

    private IEnumerator Working(float time)
    {
        _isReady = false;
        float currentTime = 0;

        AbilityEnabled?.Invoke();

        while (currentTime < time)
        {
            Enemy enemy = _zoneAttack.GetNearEnemy();

            if (enemy != null && enemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(_pointPerSecond);
                _health.Heal(_pointPerSecond);
            }

            yield return new WaitForSeconds(1f);

            currentTime++;
            ChangedFillPoint?.Invoke((time - currentTime) / time);
        }

        AbilityEnabled?.Invoke();
        StartingRecharch();
        _waitWorking = null;
    }

    private IEnumerator Recharging(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            yield return new WaitForSeconds(1f);

            currentTime++;
            ChangedFillPoint?.Invoke(currentTime / time);
        }

        _isReady = true;
        _waitRecharging = null;
    }
}
