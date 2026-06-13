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
    private Coroutine _countdown;

    private bool _isReady;

    public event Action AbilityEnabled;
    public event Action<float> ChangedFillPoint;

    private void Awake()
    {
        _isReady = false;
        _health = GetComponent<Health>();

        StartMyCorountine(_countdown, Recharging(_rechargeTime));
    }

    public void Work()
    {
        if (_isReady)
        {
            StartMyCorountine(_countdown, Working(_workingTime));
        }
    }

    private void StartMyCorountine(Coroutine coroutine, IEnumerator enumerator)
    {
        StopMyCorountine(coroutine);
        coroutine = StartCoroutine(enumerator);
    }

    private void StopMyCorountine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
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
            ChangedFillPoint?.Invoke((time-currentTime)/time);
        }

        AbilityEnabled?.Invoke();
        StartMyCorountine(_countdown, Recharging(_rechargeTime));
    }

    private IEnumerator Recharging(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
           yield return new WaitForSeconds(1f);

            currentTime++;
            ChangedFillPoint?.Invoke(currentTime/time);
        }

        _isReady = true;
    }
}
