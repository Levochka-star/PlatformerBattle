using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class AbilityVampirism : MonoBehaviour
{
    [SerializeField] private float _pointPerSecond;
    [SerializeField] private float _workingTime = 6f;
    [SerializeField] private float _rechargeTime = 4f;

    [SerializeField] private EnemyDetector _zoneAttack;

    private Health _health;

    private Coroutine _waitWorking;
    private Coroutine _waitRecharging;

    private WaitForSeconds _waitOneSecond = new WaitForSeconds(1f);

    private bool _isReady;

    public event Action<bool> AbilityEnabled;
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

        AbilityEnabled?.Invoke(true);

        while (currentTime < time)
        {
            Enemy enemy = _zoneAttack.GetNearEnemy();

            if (enemy != null && enemy.TryGetComponent(out Health enemyHealth))
            {
                float healthValue = enemyHealth.ExtractHealth(_pointPerSecond);
                _health.Heal(healthValue);
            }

            yield return _waitOneSecond;

            currentTime++;
            ChangedFillPoint?.Invoke((time - currentTime) / time);
        }

        AbilityEnabled?.Invoke(false);
        StartingRecharch();
        _waitWorking = null;
    }

    private IEnumerator Recharging(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            yield return _waitOneSecond;

            currentTime++;
            ChangedFillPoint?.Invoke(currentTime / time);
        }

        _isReady = true;
        _waitRecharging = null;
    }
}
