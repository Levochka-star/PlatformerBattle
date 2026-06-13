using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected float _maxFillPoint => _health.MaxValue;

    protected float _currentFillPercent;

    private void Awake()
    {
        if (_health != null)
            _health.ChangedHealthPoint += UpdateValue;
            _health.ChangedHealthPoint += TryDisableVisability;
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.ChangedHealthPoint -= UpdateValue;
            _health.ChangedHealthPoint -= TryDisableVisability;
    }

    protected abstract void UpdateValue(float current);

    private void TryDisableVisability(float current)
    {
        if (current <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
