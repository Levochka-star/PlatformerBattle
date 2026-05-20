using UnityEngine;

public abstract class BaseBar : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected float _maxFillPoint => _health.MaxValue;

    protected float _currentFillPercent;

    private void Awake()
    {
        if (_health != null)
            _health.ChangedHealthPoint += UpdateValue;
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.ChangedHealthPoint -= UpdateValue;
    }

    protected abstract void UpdateValue(float current);
}
