using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private ZoneAbility _zoneVampirism;

    [SerializeField] private AbilityBar _abiliityBar;

    [SerializeField] private float _pointPerSecond;

    [SerializeField] private float _workingTime = 6f;

    [SerializeField] private float _rechargeTime = 4f;

    private Health _health;

    private Coroutine _coroutine;

    private bool _isReady = true;
    private bool _isWork = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _inputReader.ChangedVampirAbility += StartDamage;
    }

    private void OnDestroy()
    {
        _inputReader.ChangedVampirAbility -= StartDamage;
    }

    private void StartRecharging()
    {
        _coroutine = StartCoroutine(RechargingVampirism(_rechargeTime));
    }

    private void StartDamage()
    {
        if (_isReady && !_isWork)
        {
            _isWork = true;
            _coroutine = StartCoroutine(WorkingVampirism(_workingTime));
        }
    }

    private IEnumerator WorkingVampirism(float time)
    {
        float pastTime = 0f;
        float valueBar = time - 1;

        _zoneVampirism.ToggleVisibility();

        while (pastTime < time)
        {
            _zoneVampirism.ApplyDamage(_pointPerSecond);

            _health.Heal(_zoneVampirism.GetDamagedTargetsCount() * _pointPerSecond);

            yield return new WaitForSeconds(1f);

            pastTime++;

            _abiliityBar.UpdateValue(valueBar - pastTime, time);
        }

        _isReady = false;
        _isWork = false;
        _zoneVampirism.ToggleVisibility();
        _coroutine = null;

        StartRecharging();
    }

    private IEnumerator RechargingVampirism(float time)
    {
        float pastTime = 0f;

        while (pastTime < time)
        {
            yield return new WaitForSeconds(1f);

            pastTime++;

            _abiliityBar.UpdateValue(pastTime, time);
        }

        _isReady = true;

        _coroutine = null;
    }
}
