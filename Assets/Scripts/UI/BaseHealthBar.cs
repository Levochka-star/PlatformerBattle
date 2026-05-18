using UnityEngine;

public abstract class BaseHealthBar : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected float _maxFillPoint => _health.MaxHealth;

    protected float _currentFillPercent;

    private void Awake()
    {
        if (_health != null)
            _health.ChangedHealthPoint += HealthView;
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.ChangedHealthPoint -= HealthView;
    }

    protected abstract void HealthView(float current);
}
